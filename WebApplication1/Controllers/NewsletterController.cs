using LabWork1.Models;
using Microsoft.AspNetCore.Mvc;
using LabWork2.Services;

namespace LabWork1.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger <NewsletterController> _logger;

        public NewsletterController(IEmailSender emailSender, ILogger<NewsletterController> logger)
        {
            _emailSender = emailSender;
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
                _emailSender.SendMail(model.Name, model.Email, model.Msg);
                _logger.LogInformation($"Mail: email is successfully sent to {model.Email}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
            }

            return RedirectToAction("Index", "Home");
           
        }
    }
}
