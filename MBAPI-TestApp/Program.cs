using MBAPI_TestApp.Services;

namespace MBAPI_TestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseMiddleware<HmacVerificationService>();   // Verify incoming requests are valid

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapGet("/", async context =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync(@"
                    <!DOCTYPE html>
                    <html>
                    <head><title>API Status</title></head>
                    <body><h1>MBAPI_TestApp is running</h1></body>
                    </html>
                ");
            });

            app.Run();
        }
    }
}
