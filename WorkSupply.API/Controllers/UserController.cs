using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkSupply.API.ModelValidation;
using WorkSupply.API.Responses;
using WorkSupply.Core.DTOs.Auth;
using WorkSupply.Core.DTOs.Pagination;
using WorkSupply.Core.Exceptions;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Models.Pagination;
using WorkSupply.Core.Query;
using WorkSupply.Core.Service;

namespace WorkSupply.API.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <remarks>
        /// Requires administrator privileges
        /// </remarks>
        /// <param name="usersQuery"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "RequireAdministrator")]
        [ValidateModelAttributes]
        [ProducesResponseType(typeof(PaginatedListDto<ApplicationUserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers(UsersQuery usersQuery)
        {
            var users = await _userService.GetUsersAsync(usersQuery);

            return Ok(_mapper.Map<PaginatedList<ApplicationUser>, PaginatedListDto<ApplicationUserDto>>(users));
        }

        /// <summary>
        /// Gets single user details
        /// </summary>
        /// <remarks>
        /// Requires administrator privileges
        /// </remarks>
        /// <param name="userId">Id of a user to get</param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        [Authorize(Policy = "RequireAdministrator")]
        [ValidateModelAttributes]
        [ProducesResponseType(typeof(ApplicationUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUser(string userId)
        {
            try
            {
                var user = await _userService.GetUserAsync(userId);

                return Ok(_mapper.Map<ApplicationUser, ApplicationUserDto>(user));
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Code = ErrorCodes.CouldNotGetUser,
                    Reason = ex.Message
                });
            }
        }
        
        // TODO: write controllers for getting user employers and employees
    }
}