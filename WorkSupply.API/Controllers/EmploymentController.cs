using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkSupply.API.ModelValidation;
using WorkSupply.API.Responses;
using WorkSupply.Core.DTOs.Employment;
using WorkSupply.Core.Exceptions;
using WorkSupply.Core.Service;

namespace WorkSupply.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Employment")]
    public class EmploymentController : Controller
    {
        private readonly IEmploymentService _employmentService;
        
        public EmploymentController(IEmploymentService employmentService)
        {
            _employmentService = employmentService;
        }

        
        /// <summary>
        /// Changes the employment status of employee towards given employer
        /// </summary>
        /// <param name="createEmploymentDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "RequireAdministrator")]
        [ValidateModelAttributes]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEmployment([FromBody] CreateEmploymentDto createEmploymentDto) 
        {
            try
            {
                await _employmentService.SetEmploymentStatus(createEmploymentDto.EmployeeId, createEmploymentDto.EmployerId,
                    createEmploymentDto.RelationshipShouldExist);

                return Ok();
            } 
            catch (EntityNotFoundException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Code = ErrorCodes.CouldNotChangeEmploymentStatus,
                    Reason = ex.Message
                });
            }   
        }
    }
}