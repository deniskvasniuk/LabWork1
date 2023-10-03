using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;



namespace LabWork2.Services
{

    public interface IMailService
    {
        void SendMail(string name, string email, string msg);
    }

    public class MailService : IMailService
    {
        readonly ILogger<MailService> _logger;
        readonly IConfiguration _configuration;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public void SendMail(string name, string email, string msg)
        {
            var settings = _configuration.GetSection("SmtpSettings");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("UroTaxi", "urotaxi.test@gmail.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Thanks for your feedback";
            message.Body = new TextPart("html")
            {
                Text = $"<h2>Дякуємо, {name}, за ваш відгук!</h2>\r\n        " +
                       $"<p>Ми отримали ваш запит на фідбек і вдячні за ваш час.</p>\r\n        " +
                       $"<blockquote style=\"background-color: #f9f9f9; border-left: 5px solid #ccc; margin: 10px 0; padding: 10px;\">\r\n{msg}</p></blockquote>\r\n"+
                       $"<p>Ми надзвичайно цінуємо вашу думку і зробимо все можливе для поліпшення наших послуг.</p>\r\n\r\n        " +
                       $"<p>З повагою,<br><b>Команда Uro Taxi</b></p>"
            };

            try
            {
                using (var client = new SmtpClient())
                {

                    var smtpServer = settings["SmtpServer"];
                    var smtpPort = int.Parse(settings["SmtpPort"]);
                    var smtpUsername = settings["SmtpUsername"];
                    var smtpPassword = settings["SmtpPassword"];

                    client.Connect(smtpServer, smtpPort, true);
                    client.Authenticate(smtpUsername, smtpPassword);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
            }
        }
    }
    
}