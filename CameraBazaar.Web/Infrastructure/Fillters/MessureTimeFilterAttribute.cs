namespace CameraBazaar.Web.Infrastructure.Fillters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Diagnostics;
    using System.IO;

    public class MessureTimeFilterAttribute : ActionFilterAttribute
    {
        private Stopwatch stopwatch;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.stopwatch = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            this.stopwatch.Stop();

            using (var writer = new StreamWriter("Action-times.txt", true))
            {
                var datetime = DateTime.UtcNow;
                var controller = context.Controller.GetType().Name;
                var action = context.RouteData.Values["action"];
                var elapsedTime = this.stopwatch.Elapsed;

                var message = $"{datetime} – {controller}.{action} – {elapsedTime}";

                writer.WriteLine(message);
            }
        }
    }
}
