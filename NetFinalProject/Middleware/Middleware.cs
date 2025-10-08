using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
namespace NetFinalProject.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // الحصول على بيانات الريكوست
            var path = context.Request.Path;
            var user = context.User.Identity.IsAuthenticated ? context.User.Identity.Name : "Anonymous";
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // تسجيل في Console
            Console.WriteLine($"[{timestamp}] User: {user}, Path: {path}");

            // أو تسجيل في ملف نصي (اختياري)
            // string logLine = $"[{timestamp}] User: {user}, Path: {path}\n";
            // await File.AppendAllTextAsync("Logs/requests.txt", logLine);

            // الاستمرار في سلسلة Middleware
            await _next(context);
        }
    }
}
