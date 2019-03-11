using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SelectionCommittee.API.Models.Faculties;
using SelectionCommittee.BLL.Enrollees.Services;
using SelectionCommittee.BLL.Faculties;
using SelectionCommittee.BLL.Faculties.Services;

namespace SelectionCommittee.API.Controllers
{
    [Route("api/[controller]")]
    public class FacultiesController : Controller
    {
        private readonly IFacultyService _facultyService;
        private readonly IEnrolleeService _enrolleeService;
        private readonly IMapper _mapper;

        public FacultiesController(IFacultyService facultyService, IMapper mapper, IEnrolleeService enrolleeService)
        {
            _facultyService = facultyService;
            _mapper = mapper;
            _enrolleeService = enrolleeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var facultyDtos = await _facultyService.GetAllAsync();
            var facultyModels = _mapper.Map<IEnumerable<FacultyModel>>(facultyDtos);
            return Ok(facultyModels);
        }

        [Route("byName(a-z)")]
        [HttpGet]
        public async Task<IActionResult> GetAllByNameFromAAsync()
        {
            var facultyDtos = await _facultyService.GetAllSortedByNameFromAAsync();
            var facultyModels = _mapper.Map<IEnumerable<FacultyModel>>(facultyDtos);
            return Ok(facultyModels);
        }

        [Route("byName(z-a)")]
        [HttpGet]
        public async Task<IActionResult> GetAllByNameFromZAsync()
        {
            var facultyDtos = await _facultyService.GetAllSortedByNameFromZAsync();
            var facultyModels = _mapper.Map<IEnumerable<FacultyModel>>(facultyDtos);
            return Ok(facultyModels);
        }

        [Route("byNumberOfPlaces")]
        [HttpGet]
        public async Task<IActionResult> GetAllByNumberOfPlacesAsync()
        {
            var facultyDtos = await _facultyService.GetAllSortedByAmountOfPlacesAsync();
            var facultyModels = _mapper.Map<IEnumerable<FacultyModel>>(facultyDtos);
            return Ok(facultyModels);
        }

        [Route("byNumberOfBudgetPlaces")]
        [HttpGet]
        public async Task<IActionResult> GetAllByNumberOfBudgetPlacesAsync()
        {
            var facultyDtos = await _facultyService.GetAllSortedByAmountofBudgetPlacesAsync();
            var facultyModels = _mapper.Map<IEnumerable<FacultyModel>>(facultyDtos);
            return Ok(facultyModels);
        }

        [HttpGet("{id}", Name = "GetFaculty")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var facultyDto = await _facultyService.GetAsync(id);
            var facultyModel = _mapper.Map<FacultyModel>(facultyDto);
            return Ok(facultyModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] FacultyAddOrUpdateModel facultyAddOrUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            var facultyDto = _mapper.Map<FacultyCreateDto>(facultyAddOrUpdateModel);
            var facultyCreateModel = await _facultyService.AddAsync(facultyDto);
            return Ok(facultyCreateModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int? id,
            [FromBody] FacultyAddOrUpdateModel facultyAddOrUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }
            if (id.HasValue)
            {
                var facultyUpdateDto = _mapper.Map<FacultyUpdateDto>(facultyAddOrUpdateModel);
                facultyUpdateDto.Id = id.Value;
                var faculty = await _facultyService.UpdateAsync(facultyUpdateDto);
                return Ok(faculty);
            }
            else
            {
                var facultyDto = _mapper.Map<FacultyCreateDto>(facultyAddOrUpdateModel);
                var facultyCreateModel = await _facultyService.AddAsync(facultyDto);
                return Ok(facultyCreateModel);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _facultyService.DeleteAsync(id);
            return Ok(response);
        }

        //[Authorize(Roles = "admin")]
        [Route("createFacultyStatementForOne")]
        [HttpGet]
        public async Task<IActionResult> CreateFacultyStatement(int id)
        {
            var enrolleeRate = await _enrolleeService.CalculateRating(id);
            return Ok(enrolleeRate);
        }

        //[Authorize(Roles = "admin")]
        [Route("createFacultyStatement")]
        [HttpGet]
        public async Task<IActionResult> CreateFacultyStatement()
        {
            var enrolleesRatings = await _enrolleeService.CalculateRatings();
            return Ok(enrolleesRatings);
        }
    }
}