using System.Security.Cryptography;
using System.Text;

namespace MBAPI_TestApp.Services
{
    public class HmacVerificationService
    {
        private readonly RequestDelegate _next;
        private readonly string _sharedSecret;

        public HmacVerificationService(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _sharedSecret = config["Settings:SharedSecret"] ?? ""; 
        }

        public async Task Invoke(HttpContext context)
        {
            // Bypass HMAC check for root status page
            var path = context.Request.Path.Value?.ToLower();
            if (path == "/")
            {
                await _next(context);
                return;
            }

            context.Request.EnableBuffering(); 
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;

            var receivedSignature = context.Request.Headers["X-Signature"].FirstOrDefault();
            if (string.IsNullOrEmpty(receivedSignature))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Missing signature");
                return;
            }

            var computedSignature = ComputeHmac(_sharedSecret, body);

            if (!CryptographicOperations.FixedTimeEquals(
                    Convert.FromBase64String(receivedSignature),
                    Convert.FromBase64String(computedSignature)))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Invalid signature");
                return;
            }

            await _next(context);
        }

        private static string ComputeHmac(string secret, string message)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secret);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            using var hmac = new HMACSHA256(keyBytes);
            var hash = hmac.ComputeHash(messageBytes);
            return Convert.ToBase64String(hash);
        }
    }
}
