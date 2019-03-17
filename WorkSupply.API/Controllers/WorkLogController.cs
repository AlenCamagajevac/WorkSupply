using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkSupply.API.ModelValidation;
using WorkSupply.API.Responses;
using WorkSupply.Core.DTOs.Auth;
using WorkSupply.Core.DTOs.Pagination;
using WorkSupply.Core.DTOs.WorkLog;
using WorkSupply.Core.Exceptions;
using WorkSupply.Core.Models.Pagination;
using WorkSupply.Core.Models.WorkLog;
using WorkSupply.Core.Query;
using WorkSupply.Core.Service;

namespace WorkSupply.API.Controllers
{
    [Produces("application/json")]
    [Route("api/WorkLog")]
    public class WorkLogController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IWorkLogService _workLogService;

        public WorkLogController(IMapper mapper, IWorkLogService workLogService)
        {
            _workLogService = workLogService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets single work log
        /// </summary>
        /// <param name="workLogId">Id of a work log</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        [ValidateModelAttributes]
        [ProducesResponseType(typeof(WorkLogDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetWorkLog([FromRoute] string id)
        {
            try
            {
                var employerId = User.FindFirst(ClaimTypes.Sid).Value;

                var workLog = await _workLogService.GetWorkLogAsync(employerId, id);

                return Ok(_mapper.Map<WorkLog, WorkLogDto>(workLog));
            }
            catch (Exception ex) when (ex is EntityNotFoundException || ex is PermissionDeniedException)
            {
                return BadRequest(new ErrorResponse
                {
                    Code = ErrorCodes.CouldNotGetWorkLog,
                    Reason = ex.Message
                });
            }
        }

        /// <summary>
        /// Get work logs for current user
        /// </summary>
        /// <param name="filters">list of filters passed as query params</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ValidateModelAttributes]
        [ProducesResponseType(typeof(PaginatedListDto<WorkLogDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetWorkLogs(WorkLogQuery filters)
        {
            var userId = User.FindFirst(ClaimTypes.Sid).Value;

            var workLogs = await _workLogService.GetWorkLogs(filters, userId);

            return Ok(_mapper.Map<PaginatedList<WorkLog>, PaginatedListDto<WorkLogDto>>(workLogs));
        }

        /// <summary>
        /// Creates a new work log
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        [HttpPost("")]
        [Authorize(Policy = "RequireEmployee")]
        [ValidateModelAttributes]
        [ProducesResponseType(typeof(WorkLogDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateWorkLog([FromBody] CreateWorkLogDto createDto)
        {
            try
            {
                var workLog = _mapper.Map<CreateWorkLogDto, WorkLog>(createDto);

                await _workLogService.SaveWorkLog(workLog);

                return CreatedAtAction(
                    nameof(GetWorkLog),
                    new {id = workLog.Id},
                    _mapper.Map<WorkLog, WorkLogDto>(workLog));
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Code = ErrorCodes.CouldNotCreateWorkLog,
                    Reason = ex.Message
                });
            }
        }
        
        /// <summary>
        /// Resolves a work log
        /// </summary>
        /// <param name="resolveDto"></param>
        /// <returns></returns>
        [HttpPost("Resolve")]
        [Authorize(Policy = "RequireEmployer")]
        [ValidateModelAttributes]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResolveWorkLog([FromBody] ResolveWorkLogDto resolveDto)
        {
            try
            {
                var employerId = User.FindFirst(ClaimTypes.Sid).Value;

                await _workLogService.ResolveWorkLog(employerId, resolveDto.WorkLogId, resolveDto.WorkLogStatus);

                return Ok();
            }
            catch (Exception ex) when (
                ex is EntityNotFoundException ||
                ex is PermissionDeniedException ||
                ex is EntityAlreadyModifiedException)
            {
                return BadRequest(new ErrorResponse
                {
                    Code = ErrorCodes.CouldNotResolveWorkLog,
                    Reason = ex.Message
                });
            }
        }
    }
}