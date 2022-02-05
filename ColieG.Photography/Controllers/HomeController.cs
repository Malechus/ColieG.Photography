using ColieG.Photography.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;

namespace ColieG.Photography.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            ContactModel _model = new ContactModel();

            return View(_model);
        }

        [HttpPost]
        public IActionResult Contact(ContactModel _modelFilled)
        {
            ContactModel _model = _modelFilled;

            string textConsent;

            if (_model.Text)
            {
                textConsent = "Yes";
            }
            else
            {
                textConsent = "No";
            }

            using(MailMessage message = new MailMessage())
            {
                message.From = new MailAddress("garrett@malechus.com");

                //message.To.Add(new MailAddress("linzerrz@gmail.com"));
                message.To.Add(new MailAddress("garrett@malechus.com"));

                message.Subject = @"Website Contact";

                message.IsBodyHtml = true;
                message.Body = "A new contact form has been submitted from your website, please see below." +
                    "<hr />" +
                    "<br />" +
                    "<p>Name : " + _model.Name + "</p>" +
                    "<p>Email : " + _model.Email + "</p>" +
                    "<p>Phone : " + _model.Phone + "</p>" +
                    "<p>Consent to Text : " + textConsent + "</p>" +
                    "<br />" +
                    "<p>Message : </p>" +
                    _model.Message +
                    "<br />" +
                    "<br />" +
                    "<hr />" +
                    "<p>This is an automatically generated email sent by your webmaster. You can reply if you need assistance.";

                using (SmtpClient client = new SmtpClient("localhost:587"))
                {

                    client.Send(message);
                }
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}