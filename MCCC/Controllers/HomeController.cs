using System.Diagnostics;
using MCCC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MCCC.Controllers
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

        public IActionResult Catalog()
        {
            return View();
        }

        public IActionResult AvailableNow()
        {
            return View();
        }

        public IActionResult ContactMe()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact(string name, string address1, string address2, string postalCode, string email, List<string> itemDesc, List<string> colour, List<IFormFile> itemPhoto)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("megscozycrochetcrafts@gmail.com");
            message.To.Add("megan.summers16@outlook.com");
            message.Subject = "New Crochet Order";
            string body = "";
            body += $"Name: {name}\n";
            body += $"Email: {email}\n";
            body += $"Items:\n";
            for (int i = 0; i < itemDesc.Count; i++)
            {
                body += $"\nItem: {i + 1}\n";
                body += $"Description: {itemDesc[i]}\n";
                body += $"Colour: {colour[i]}\n";
            }
            message.Body = body;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new NetworkCredential("megscozycrochetcrafts@gmail.com", "iylyfgsdtkpppjsp");
            client.EnableSsl = true;
            await client.SendMailAsync(message);
            return RedirectToAction("Index");
        }

        public IActionResult Colours()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
