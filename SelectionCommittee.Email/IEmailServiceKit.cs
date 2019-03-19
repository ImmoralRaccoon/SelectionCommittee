using System.Threading.Tasks;

namespace SelectionCommittee.Email
{
    public interface IEmailServiceKit
    {
        /// <summary>
        /// Sends email.
        /// </summary>
        /// <param name="email">Enrollee email</param>
        Task SendEmailAsync(string email);
    }
}