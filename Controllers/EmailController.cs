using BookRental_dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using BookRental_dotnet.Data;

namespace BookRental_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : Controller
    {

        private readonly BookAPIDbContext dbContext;

        public EmailController(BookAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult SendEmail(NewEmail newEmail)
        {
            var email = new MimeMessage();
            var body = "<h3>Welkom bij WT Boeken Reserveren</h3></br><p><a href='http://localhost:3000/register'>Klik hier</a> om je te registreren</p>";
            email.From.Add(MailboxAddress.Parse("noreply@workingtalent.com"));
            email.To.Add(MailboxAddress.Parse(newEmail.email));
            email.Subject = "Welkom bij WT Boeken Reserveren";
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("winona85@ethereal.email", "Y365eEaX1PkJp1TEqx");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();
        }


    }
}