using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
