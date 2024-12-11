using System.Threading.Tasks;

namespace Abituriyent.Info.Web.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
