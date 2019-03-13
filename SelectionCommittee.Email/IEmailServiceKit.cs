using System.Threading.Tasks;

namespace SelectionCommittee.Email
{
    public interface IEmailServiceKit
    {
        Task SendEmailAsync(string email);
    }
}