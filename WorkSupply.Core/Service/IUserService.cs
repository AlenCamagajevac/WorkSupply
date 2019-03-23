using System.Threading.Tasks;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Models.Pagination;
using WorkSupply.Core.Query;

namespace WorkSupply.Core.Service
{
    public interface IUserService
    {
        /// <summary>
        /// Gets all users in role paginated
        /// </summary>
        /// <param name="usersQuery"></param>
        /// <returns></returns>
        Task<PaginatedList<ApplicationUser>> GetUsersAsync(UsersQuery usersQuery);

        /// <summary>
        /// Gets single user async
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ApplicationUser> GetUserAsync(string userId);
    }
}