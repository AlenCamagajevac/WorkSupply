using System.Threading.Tasks;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Models.Token;

namespace WorkSupply.Core.Service
{
    public interface IAuthService
    {
        /// <summary>
        /// Creates a user and sends a conformation mail
        /// User will get link where he can activate his account(code) and using that link(code)
        /// he can change his password 
        /// </summary>
        /// <param name="user">User info</param>
        /// <param name="role">Role in witch user will be added</param>
        /// <returns></returns>
        Task<bool> CreateUserAsync(ApplicationUser user, Role role);

        /// <summary>
        /// Creates Jwt token witch user can use to authenticate requests
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<Jwt> CreateJwtToken(string email, string password);

        /// <summary>
        /// Given the emailConfirmation code change user password and set email active to true
        /// Once completed, user can use new password to log in
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <param name="emailConfirmationCode"></param>
        /// <returns></returns>
        Task<bool> ChangeUserPassword(string userId, string newPassword, string emailConfirmationCode);
    }
}