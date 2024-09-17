using Microsoft.AspNetCore.Mvc;

namespace MyApp.Web.Controllers.Admin
{
    [Route("admin/[controller]")]
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
