namespace CameraBazaar.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        public void SetError(string message) => TempData["error"] = message;
    }
}
