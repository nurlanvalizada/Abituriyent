using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Abituriyent.Info.Web.Services
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Your Name", "your email"));
            emailMessage.To.Add(new MailboxAddress(email));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder {HtmlBody = message};
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                int yourPort = 587;
                client.Connect("smtp_host", yourPort, SecureSocketOptions.StartTls);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("username", "pass");
                client.Send(emailMessage);
                client.Disconnect(true);
            }

            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
