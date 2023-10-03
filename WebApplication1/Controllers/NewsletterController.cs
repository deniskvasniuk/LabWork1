using LabWork1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using LabWork2.Services;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

namespace YourNamespace.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly ILogger <NewsletterController> _logger;

        public NewsletterController(IConfiguration configuration, IMailService mailService, ILogger<NewsletterController> logger)
        {
            _configuration = configuration;
            _mailService = mailService;
            _logger = logger;

        }

        [HttpPost]
        public IActionResult Subscribe(MailFormModel model) 
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            try
            {
                _mailService.SendMail(model.Name, model.Email, model.Msg);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
            }

            return RedirectToAction("Index", "Home");
           
        }
    }
}
