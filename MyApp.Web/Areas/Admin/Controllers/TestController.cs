using Microsoft.AspNetCore.Mvc;

namespace MyApp.Web.Areas.Admin.Controllers
{
    [Route("admin/[controller]")]
    [Area("Admin")]
    public class TestController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("create")]
        public IActionResult CreatePost()
        {
            return View("create");
        }
    }
}
