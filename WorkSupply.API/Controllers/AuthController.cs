using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkSupply.API.Responses;
using WorkSupply.Core.DTOs.Auth;
using WorkSupply.Core.Exceptions;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Models.Token;
using WorkSupply.Core.Service;

namespace WorkSupply.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AuthController(IMapper mapper, IAuthService authService)
        {
            _mapper = mapper;
            _authService = authService;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            var user = _mapper.Map<RegisterDto, ApplicationUser>(registerDto);

            if (!await _authService.CreateUserAsync(user, registerDto.Password))
            {
                return BadRequest(new ErrorResponse
                {
                    Code = ErrorCodes.CouldNotCreateUser,
                    Reason = $"Could not Create user"
                });
            }

            return Ok(_mapper.Map<ApplicationUser, UserDto>(user));
        }
        
        /// <summary>
        /// Set user role
        /// </summary>
        /// <param name="assignDto"></param>
        /// <returns></returns>
        // TODO: Maybe only some admin or such can add user to role. Default role maybe? 
        [HttpPost("AssignToRole")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AssignUserToRole([FromBody] AssignToRoleDto assignDto)
        {
            try
            {
                var assignSuccess = await _authService.AssignUserToRoleAsync(assignDto.UserId, assignDto.RoleId);
                if (assignSuccess)
                {
                    return Ok();
                }

                return BadRequest(new ErrorResponse
                {
                    Code = ErrorCodes.CouldNotAddUserToRole,
                    Reason = $"Could not assign user to a role"
                });
            }
            catch (Exception ex) when(ex is UserAlreadyInRoleException || ex is EntityNotFoundException)
            {
                return BadRequest(new ErrorResponse
                {
                    Code = ErrorCodes.CouldNotAddUserToRole,
                    Reason = ex.Message
                });
            }
        }
        
        /// <summary>
        /// Create Jwt token for a user
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(JwtTokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var jwt = await _authService.CreateJwtToken(loginDto.Email, loginDto.Password);

                return Ok(_mapper.Map<Jwt, JwtTokenDto>(jwt));
            }
            catch (CouldNotLogInUserException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Code = ErrorCodes.CouldNotAddUserToRole,
                    Reason = ex.Message
                });
            }
        }
    }
}