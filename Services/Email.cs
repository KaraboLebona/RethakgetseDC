using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using DayCareProject.Models;

namespace DayCareProject.Services
{
    public class EmailService
    {
        private readonly string _smtpServer = "smtp.yourserver.com";
        private readonly int _port = 587;
        private readonly string _username = "your_email@domain.com";
        private readonly string _password = "your_password";

        public async Task SendApplicationEmailAsync(ChildApplication model)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("DayCare System", _username));
            message.To.Add(new MailboxAddress("Admin", "admin@daycare.com"));
            message.Subject = "New Child Application Submitted";

            message.Body = new TextPart("html")
            {
                Text = $"<h3>New Child Application Submitted</h3>" +
                       $"<p>Child Full Name: {model.ChildFullName} {model.ChildSurname}</p>" +
                       $"<p>Date of Birth: {model.DateOfBirth.ToShortDateString()}</p>" +
                       $"<p>Home Language: {model.HomeLanguage}</p>" +
                       $"<p>Residential Address: {model.ResidentialAddress}</p>" +
                       $"<p>Parent Full Name: {model.ParentFullName} {model.ParentSurname}</p>" +
                       $"<p>Occupation: {model.Occupation}</p>" +
                       $"<p>Work Tel: {model.WorkTel}</p>" +
                       $"<p>Cell: {model.Cell}</p>" +
                       $"<p>Next of Kin: {model.KinName} {model.KinSurname} ({model.KinRelation})</p>" +
                       $"<p>Date Submitted: {model.DateSubmitted}</p>"
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _port, false);
                await client.AuthenticateAsync(_username, _password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
