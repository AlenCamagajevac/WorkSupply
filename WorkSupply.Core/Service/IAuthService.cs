using System.Threading.Tasks;
using WorkSupply.Core.DTOs.Auth;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Models.Token;

namespace WorkSupply.Core.Service
{
    public interface IAuthService
    {
        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> CreateUserAsync(ApplicationUser user, string password);

        /// <summary>
        /// Adds user to a role
        /// </summary>
        /// <param name="userId">Id of a user</param>
        /// <param name="roleId">Id of a role</param>
        /// <returns></returns>
        Task<bool> AssignUserToRoleAsync(string userId, string roleId);

        /// <summary>
        /// Creates Jwt token witch user can use to authenticate requests
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<Jwt> CreateJwtToken(string email, string password);
    }
}