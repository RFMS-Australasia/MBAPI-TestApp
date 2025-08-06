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
            // Bypass HMAC check for root status page and health checks
            var path = context.Request.Path.Value?.ToLower();
            if (path == "/" || path == "/test" || path == "/webhook/health")
            {
                await _next(context);
                return;
            }

            // Only verify HMAC for webhook endpoints
            if (!path?.StartsWith("/webhook") == true)
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

            // Verify sha256= prefixed signature
            if (!receivedSignature.StartsWith("sha256="))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Signature must start with 'sha256='");
                return;
            }

            var signatureToVerify = receivedSignature.Substring(7); // Remove "sha256=" prefix
            var computedSignature = ComputeHmac(_sharedSecret, body);
            
            if (!CryptographicOperations.FixedTimeEquals(
                    Encoding.UTF8.GetBytes(signatureToVerify.ToLower()),
                    Encoding.UTF8.GetBytes(computedSignature.ToLower())))
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
            return Convert.ToHexString(hash).ToLower();
        }
    }
}