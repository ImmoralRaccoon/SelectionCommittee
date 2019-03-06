using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelectionCommittee.API.Models.Enrollees;
using SelectionCommittee.BLL.Enrollees;
using SelectionCommittee.BLL.Enrollees.Services;
using SelectionCommittee.DAL.EF;
using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.API.Controllers
{
    [Route("api/[controller]")]
    public class EnrolleeController : Controller
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IMapper _mapper;

        public EnrolleeController(ApplicationDbContext context, IEnrolleeService enrolleeService, IMapper mapper)
        {
            _enrolleeService = enrolleeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var enrolleeDtos = await _enrolleeService.GetAllAsync();
            var enrolleeModels = _mapper.Map<IEnumerable<EnrolleeModel>>(enrolleeDtos);
            return Ok(enrolleeModels);
        }

        [HttpGet("{id}", Name = "GetEnrollee")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var enrolleeDto = await _enrolleeService.GetAsync(id);
            var enrolleeModel = _mapper.Map<EnrolleeModel>(enrolleeDto);
            return Ok(enrolleeModel);
        }

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _enrolleeService.DeleteAsync(id);
            return Ok(response);
        }
    }
}