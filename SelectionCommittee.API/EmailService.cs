using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SelectionCommittee.API
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmail(string email, string subject, string message)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "litopisecnestor@gmail.com",
                    Password = "AdidasRun2016"
                };

                client.Credentials = credential;
                client.Host = "smtp.gmail.com";
                client.Port = int.Parse("587");
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(email));
                    emailMessage.From = new MailAddress("litopisecnestor@gmail.com");
                    emailMessage.Subject = subject;
                    emailMessage.Body = message;
                    client.Send(emailMessage);
                }
            }
            await Task.CompletedTask;
        }
    }
}