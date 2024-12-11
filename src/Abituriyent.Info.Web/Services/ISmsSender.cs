using System.Threading.Tasks;

namespace Abituriyent.Info.Web.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
