using Microsoft.AspNetCore.Mvc;

namespace MyApp.Web.Areas.Admin.Controllers
{
    [Route("admin/[controller]")]
    [Area("Admin")]
    public class SystemInforController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
