using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace SelectionCommittee.Email
{
    public class EmailServiceKit : IEmailServiceKit
    {
        public async Task SendEmailAsync(string email)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Site administration", "login@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "SelectionCommittee.API-MailKit";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "Congratulations! You are enrolled!"
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("litopisecnestor@gmail.com", "AdidasRun2016");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}