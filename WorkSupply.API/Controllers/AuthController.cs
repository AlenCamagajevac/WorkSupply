﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkSupply.API.ModelValidation;
using WorkSupply.API.Responses;
using WorkSupply.Core.DTOs.Auth;
using WorkSupply.Core.Exceptions;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Models.Token;
using WorkSupply.Core.Models.WorkLog;
using WorkSupply.Core.Persistence;
using WorkSupply.Core.Service;

namespace WorkSupply.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IMapper mapper, IAuthService authService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _authService = authService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <remarks>
        /// Once new user is created mail will be sent to given address with code to activate account
        /// Until account is activated it cannot be used
        /// To activate account use /Activate endpoint.
        /// </remarks>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        [Authorize(Policy = "RequireAdministrator")]
        [ValidateModelAttributes]
        [ProducesResponseType(typeof(ApplicationUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            var user = _mapper.Map<RegisterDto, ApplicationUser>(registerDto);

            try
            {
                if (!await _authService.CreateUserAsync(user, registerDto.Role))
                {
                    return BadRequest(new ErrorResponse
                    {
                        Code = ErrorCodes.CouldNotCreateUser,
                        Reason = $"Could not Create user"
                    });
                }
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Code = ErrorCodes.CouldNotCreateUser,
                    Reason = ex.Message
                });
            }

            return Ok(_mapper.Map<ApplicationUser, ApplicationUserDto>(user));
        }

        /// <summary>
        /// Sends a request to activate user account
        /// </summary>
        /// <remarks>
        /// This will set new password and allow user to log in with it
        /// </remarks>
        /// <param name="activateDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [ValidateModelAttributes]
        [HttpPost("Activate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActivateUserAccount([FromBody] ActivateAccountDto activateDto)
        {
            try
            {
                var activationSuccess = await _authService.ChangeUserPassword(activateDto.UserId,
                    activateDto.NewPassword, activateDto.EmailConfirmationToken);

                if (activationSuccess)
                {
                    return Ok();
                }

                return BadRequest(new ErrorResponse
                {
                    Code = ErrorCodes.CouldNotActivateUserAccount,
                    Reason = "Error activating user account, please contact support!"
                });
            }
            catch (Exception ex) when (ex is EntityNotFoundException || ex is PermissionDeniedException)
            {
                return BadRequest(new ErrorResponse
                {
                    Code = ErrorCodes.CouldNotActivateUserAccount,
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
        [ValidateModelAttributes]
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
                    Code = ErrorCodes.CouldNotLogInUser,
                    Reason = ex.Message
                });
            }
        }
    }
}