using Microsoft.AspNetCore.Mvc;

namespace MyApp.Web.Controllers
{
    public class SystemInforController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
