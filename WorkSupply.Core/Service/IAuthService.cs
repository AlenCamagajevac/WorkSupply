using System.Threading.Tasks;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Models.Token;

namespace WorkSupply.Core.Service
{
    public interface IAuthService
    {
        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="user">User info</param>
        /// <param name="password">User password</param>
        /// <param name="role">Role in witch user will be added</param>
        /// <returns></returns>
        Task<bool> CreateUserAsync(ApplicationUser user, string password, Role role);

        /// <summary>
        /// Creates Jwt token witch user can use to authenticate requests
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<Jwt> CreateJwtToken(string email, string password);
    }
}