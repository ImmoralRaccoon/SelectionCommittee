using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SelectionCommittee.API.Models.Enrollees;
using SelectionCommittee.API.Services.Enrollees;
using SelectionCommittee.BLL.Enrollees;
using SelectionCommittee.BLL.Enrollees.Services;
using SelectionCommittee.DAL.EF;
using SelectionCommittee.Email;

namespace SelectionCommittee.API.Controllers
{
    [Route("api/[controller]")]
    public class EnrolleeController : Controller
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IMapper _mapper;
        private readonly IEmailServiceKit _emailServiceKit;
        private readonly IEnrolleeResponseComposer _enrolleeResponseComposer;

        public EnrolleeController(ApplicationDbContext context, IEnrolleeService enrolleeService, IMapper mapper, IEmailServiceKit emailServiceKit, IEnrolleeResponseComposer enrolleeResponseComposer)
        {
            _enrolleeService = enrolleeService;
            _mapper = mapper;
            _emailServiceKit = emailServiceKit;
            _enrolleeResponseComposer = enrolleeResponseComposer;
        }

        /// <summary>
        /// Get all enrolles.
        /// </summary>
        /// <returns>Returns all enrolles</returns>
        /// <response code="200">Always</response>
        [Authorize(Roles = "user")]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllAsync()
        {
            var enrolleeDtos = await _enrolleeService.GetAllAsync();
            var response = _enrolleeResponseComposer.ComposeForGetAll(enrolleeDtos);
            return response;
        }

        /// <summary>
        /// Get enrollee by id.
        /// </summary>
        /// <param name="id">Enrollee id</param>
        /// <returns>Returns enrollee by id</returns>
        /// <response code="200">Always</response>
        /// <response code="404">If the item is not found</response>
        [Authorize(Roles = "user")]
        [HttpGet("{id}", Name = "GetEnrollee")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var enrolleeDto = await _enrolleeService.GetAsync(id);
            var response = _enrolleeResponseComposer.ComposeForGet(enrolleeDto);
            return response;
        }

        /// <summary>
        /// Creates an enrollee.
        /// </summary>
        /// <param name="enrolleeAddOrUpdateModel">Enrollee model</param>
        /// <returns>Returns route to created enrollee</returns>
        /// <response code="201">If the item created</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        [Authorize(Roles = "user")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddAsync([FromBody] EnrolleeAddModel enrolleeAddOrUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var enrolleeCreateDto = _mapper.Map<EnrolleCreateDto>(enrolleeAddOrUpdateModel);
            var statusCode = await _enrolleeService.AddAsync(enrolleeCreateDto);
            var response = _enrolleeResponseComposer.ComposeForCreate(statusCode, enrolleeCreateDto);
            return response;
        }

        /// <summary>
        /// Add faculty to enrollee.
        /// </summary>
        /// <param name="facultyEnrolleeModel">Faculty enrollee model</param>
        /// <returns></returns>
        /// <response code="200">If operation is seccessful</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        [Authorize(Roles = "user")]
        [Route("decouplingTable")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddFacultyToEnrolleeAsync([FromBody] FacultyEnrolleeAddModel facultyEnrolleeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var facultyEnrolleeCreateDto = _mapper.Map<FacultyEnrolleeCreateDto>(facultyEnrolleeModel);
            var statusCode = await _enrolleeService.AddFacultyEnrolleeAsync(facultyEnrolleeCreateDto);
            var response = _enrolleeResponseComposer.ComposeForDecouplingTable(statusCode);
            return response;
        }

        /// <summary>
        /// Updates an enrollee.
        /// </summary>
        /// <param name="id">Enrollee id</param>
        /// <param name="enrolleeAddOrUpdateModel">Enrollee add or update model</param>
        /// <response code="204">If the item updated</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        [Authorize(Roles = "user")]
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateAsync(int? id,
            [FromBody] EnrolleeUpdateModel enrolleeAddOrUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (id.HasValue)
            {
                var enrolleeUpdateDto = _mapper.Map<EnrolleeUpdateDto>(enrolleeAddOrUpdateModel);
                enrolleeUpdateDto.Id = id.Value;
                var statusCode = await _enrolleeService.UpdateAsync(enrolleeUpdateDto);
                var response = _enrolleeResponseComposer.ComposeForUpdate(statusCode);
                return response;
            }
            else
            {
                var enrolleeCreateDto = _mapper.Map<EnrolleCreateDto>(enrolleeAddOrUpdateModel);
                var statusCode = await _enrolleeService.AddAsync(enrolleeCreateDto);
                var response = _enrolleeResponseComposer.ComposeForCreate(statusCode, enrolleeCreateDto);
                return response;
            }
        }

        /// <summary>
        /// Updates enrollee status.
        /// </summary>
        /// <param name="id">Enrollee id</param>
        /// <param name="enrolleeUpdateStatusModel">Enrollee update status model</param>
        /// <response code="200">If operation is seccessful</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        [Authorize(Roles = "admin")]
        [Route("updateUserStatus")]
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateStatusAsync(int? id, [FromBody] EnrolleeUpdateStatusModel enrolleeUpdateStatusModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (id.HasValue)
            {
                var enrolleeUpdateDto = _mapper.Map<EnrolleeUpdateStatusDto>(enrolleeUpdateStatusModel);
                enrolleeUpdateDto.Id = id.Value;
                var statusCode = await _enrolleeService.UpdateStatusAsync(enrolleeUpdateDto);
                var response = _enrolleeResponseComposer.ComposeForUpdateStatus(statusCode);
                return response;
            }

            return BadRequest();
        }

        /// <summary>
        /// Deletes enrollee.
        /// </summary>
        /// <param name="id">Enrollee id</param>
        /// <response code="204">If the item deleted</response>
        /// <response code="404">If the item not found</response>
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var statusCode = await _enrolleeService.DeleteAsync(id);
            var response = _enrolleeResponseComposer.ComposeForDelete(statusCode);
            return response;
        }

        /// <summary>
        /// Sends message to enrolle.
        /// </summary>
        /// <param name="id">Enrollee id</param>
        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("Kit/send-email")]
        public async Task<IActionResult> SendMessage(int id)
        {
            var email = await _enrolleeService.GetEnrolleEmail(id);
            await _emailServiceKit.SendEmailAsync(email);
            return Ok();
        }
    }
}