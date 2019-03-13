using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SelectionCommittee.API.Models.Enrollees;
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

        public EnrolleeController(ApplicationDbContext context, IEnrolleeService enrolleeService, IMapper mapper, IEmailServiceKit emailServiceKit)
        {
            _enrolleeService = enrolleeService;
            _mapper = mapper;
            _emailServiceKit = emailServiceKit;
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var enrolleeDtos = await _enrolleeService.GetAllAsync();
            var enrolleeModels = _mapper.Map<IEnumerable<EnrolleeModel>>(enrolleeDtos);
            return Ok(enrolleeModels);
        }

        [Authorize(Roles = "user")]
        [HttpGet("{id}", Name = "GetEnrollee")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var enrolleeDto = await _enrolleeService.GetAsync(id);
            var enrolleeModel = _mapper.Map<EnrolleeModel>(enrolleeDto);
            return Ok(enrolleeModel);
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] EnrolleeAddModel enrolleeAddOrUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var enrolleeCreateDto = _mapper.Map<EnrolleCreateDto>(enrolleeAddOrUpdateModel);
            var enrolleeCreateModel = await _enrolleeService.AddAsync(enrolleeCreateDto);
            return Ok(enrolleeCreateModel);
        }

        [Authorize(Roles = "user")]
        [Route("decouplingTable")]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] FacultyEnrolleeAddModel facultyEnrolleeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var facultyEnrolleeCreateDto = _mapper.Map<FacultyEnrolleeCreateDto>(facultyEnrolleeModel);
            var facultyEnrolleeCreateModel = await _enrolleeService.AddFacultyEnrolleeAsync(facultyEnrolleeCreateDto);
            return Ok(facultyEnrolleeCreateModel);
        }

        [Authorize(Roles = "user")]
        [HttpPut]
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
                var enrollee = await _enrolleeService.UpdateAsync(enrolleeUpdateDto);
                return Ok(enrollee);
            }
            else
            {
                var enrolleeCreateDto = _mapper.Map<EnrolleCreateDto>(enrolleeAddOrUpdateModel);
                var enrolleeCreateModel = await _enrolleeService.AddAsync(enrolleeCreateDto);
                return Ok(enrolleeCreateModel);
            }
        }

        [Authorize(Roles = "admin")]
        [Route("updateUserStatus")]
        [HttpPut]
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
                var enrollee = await _enrolleeService.UpdateStatusAsync(enrolleeUpdateDto);
                return Ok(enrollee);
            }

            return BadRequest();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _enrolleeService.DeleteAsync(id);
            return Ok(response);
        }

        [HttpPost]
        [Route("Kit/send-email")]
        public async Task<IActionResult> SendMessage()
        {
            await _emailServiceKit.SendEmailAsync("kirianenko.vladislav@gmail.com");
            return Ok();
        }
    }
}