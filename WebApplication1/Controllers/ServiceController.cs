using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
