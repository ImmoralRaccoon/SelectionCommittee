using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SelectionCommittee.Authentication.Services
{
    public interface IAuthentificationService
    {
        /// <summary>
        /// User log in.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="password">User password</param>
        /// <returns>Returns succeess of operation</returns>
        Task<bool> LogIn(string email, string password);

        /// <summary>
        /// User log out.
        /// </summary>
        Task LogOut();

        /// <summary>
        /// Register user.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="password">User password</param>
        /// <param name="confirmPassword">User password for confirmation</param>
        /// <param name="roles">User roles</param>
        /// <returns>Returns collection of errors</returns>
        Task<IEnumerable<IdentityError>> Register(string email, string password, string confirmPassword,
            params string[] roles);
    }
}