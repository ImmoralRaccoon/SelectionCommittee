using System.Threading.Tasks;

namespace SelectionCommittee.API
{
    public interface IEmailService
    {
        Task SendEmail(string email, string subject, string message);
    }
}