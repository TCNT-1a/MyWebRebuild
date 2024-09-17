using Microsoft.AspNetCore.Mvc;

namespace MyApp.Web.Controllers.Admin
{
    [Route("admin/[controller]")]
    public class SystemInforController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
