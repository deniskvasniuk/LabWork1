using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LabWork1.Models;
using WebApplication1.Models;
using LabWork2.Services;
using LabWork1.Models;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IMailService mailService)
        {
            _logger = logger;
            _configuration = configuration;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            return View(new MailFormModel());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}