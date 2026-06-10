using System.Diagnostics;
using MCCC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ExcelDataReader;
using System.Data;
using MCCC.Models;

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
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "catalog.xlsx");
            List<Product> products = new List<Product>();
            using var stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);
            var result = reader.AsDataSet();
            DataTable table = result.Tables[0];
            for (int i = 1; i < table.Rows.Count; i++)
            {
                products.Add(new Product
                {
                    Name = table.Rows[i][0].ToString(),
                    Price = table.Rows[i][1].ToString(),
                    Image = table.Rows[i][2].ToString()
                });
            }
            return View(products);
        }

        public IActionResult AboutMe()
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
            body += $"\nAddress:\n";
            body += $"{address1}\n";
            body += $"{address2}\n";
            body += $"{postalCode}\n";
            body += $"\nItems:\n";
            for (int i = 0; i < itemDesc.Count; i++)
            {
                body += $"Item: {i + 1}\n";
                body += $"Description: {itemDesc[i]}\n";
                body += $"Colour: {colour[i]}\n";
                if (i < itemPhoto.Count && itemPhoto[i] != null && itemPhoto[i].Length > 0)
                {
                    var stream = itemPhoto[i].OpenReadStream();
                    var attachment = new Attachment(stream, itemPhoto[i].FileName);
                    message.Attachments.Add(attachment);
                }
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
