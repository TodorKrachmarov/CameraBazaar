namespace CameraBazaar.Web.Infrastructure.Fillters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.IO;

    public class LogFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            using(var writer = new StreamWriter("logs.txt", true))
            {
                var datetime = DateTime.UtcNow;
                var ip = context.HttpContext.Connection.RemoteIpAddress;
                var userName = context.HttpContext.User?.Identity?.Name ?? "Anonymous";
                var controller = context.Controller.GetType().Name;
                var action = context.RouteData.Values["action"];

                var message = $"{datetime} – {ip} – {userName} – {controller}.{action}";

                if (context.Exception != null)
                {
                    var exType = context.Exception.GetType().Name;
                    var exMessage = context.Exception.Message;

                    message = $"[!] {message} - {exType} – {exMessage}";
                }

                writer.WriteLine(message);
            }
        }
    }
}
