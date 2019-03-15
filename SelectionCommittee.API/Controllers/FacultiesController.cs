using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SelectionCommittee.API.Models;
using SelectionCommittee.API.Models.Faculties;
using SelectionCommittee.BLL.Enrollees.Services;
using SelectionCommittee.BLL.Faculties;
using SelectionCommittee.BLL.Faculties.Services;
using SelectionCommittee.Logger;

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

        /// <summary>
        /// Get all faculties.
        /// </summary>
        /// <returns>Returns all faculties</returns>
        /// <response code="200">Always</response>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var facultyDtos = await _facultyService.GetAllAsync();
            var facultyModels = _mapper.Map<IEnumerable<FacultyModel>>(facultyDtos);
            return Ok(facultyModels);
        }

        /// <summary>
        /// Get all faculties sorted by name(a-z).
        /// </summary>
        /// <returns>Returns all faculties sorted by name(a-z)</returns>
        /// <response code="200">Always</response>
        [Route("byName(a-z)")]
        [HttpGet]
        public async Task<IActionResult> GetAllByNameFromAAsync()
        {
            var facultyDtos = await _facultyService.GetAllSortedByNameFromAAsync();
            var facultyModels = _mapper.Map<IEnumerable<FacultyModel>>(facultyDtos);
            return Ok(facultyModels);
        }

        /// <summary>
        /// Get all faculties sorted by name(z-a).
        /// </summary>
        /// <returns>Returns all faculties sorted by name(a-z)</returns>
        /// <response code="200">Always</response>
        [Route("byName(z-a)")]
        [HttpGet]
        public async Task<IActionResult> GetAllByNameFromZAsync()
        {
            var facultyDtos = await _facultyService.GetAllSortedByNameFromZAsync();
            var facultyModels = _mapper.Map<IEnumerable<FacultyModel>>(facultyDtos);
            return Ok(facultyModels);
        }

        /// <summary>
        /// Get all faculties sorted by number of places.
        /// </summary>
        /// <returns>Returns all faculties sorted by number of places</returns>
        /// <response code="200">Always</response>
        [Route("byNumberOfPlaces")]
        [HttpGet]
        public async Task<IActionResult> GetAllByNumberOfPlacesAsync()
        {
            var facultyDtos = await _facultyService.GetAllSortedByAmountOfPlacesAsync();
            var facultyModels = _mapper.Map<IEnumerable<FacultyModel>>(facultyDtos);
            return Ok(facultyModels);
        }

        /// <summary>
        /// Get all faculties sorted by number of budjet places.
        /// </summary>
        /// <returns>Returns all faculties sorted by number of budjet places</returns>
        /// <response code="200">Always</response>
        [Route("byNumberOfBudgetPlaces")]
        [HttpGet]
        public async Task<IActionResult> GetAllByNumberOfBudgetPlacesAsync()
        {
            var facultyDtos = await _facultyService.GetAllSortedByAmountofBudgetPlacesAsync();
            var facultyModels = _mapper.Map<IEnumerable<FacultyModel>>(facultyDtos);
            return Ok(facultyModels);
        }

        /// <summary>
        /// Get faculty by id.
        /// </summary>
        /// <param name="id">Faculty id</param>
        /// <returns>Faculty by id</returns>
        /// <response code="200">If the item exists</response>
        /// <response code="404">If the item is not found</response>
        [HttpGet("{id}", Name = "GetFaculty")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var facultyDto = await _facultyService.GetAsync(id);
            var facultyModel = _mapper.Map<FacultyModel>(facultyDto);
            return Ok(facultyModel);
        }

        /// <summary>
        /// Creates a faculty.
        /// </summary>
        /// <param name="facultyAddOrUpdateModel">Faculty model</param>
        /// <returns>Returns route to created faculty</returns>
        /// <response code="201">If the item created</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
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

        /// <summary>
        /// Uptades faculty.
        /// </summary>
        /// <param name="id">Faculty id</param>
        /// <param name="facultyAddOrUpdateModel">Faculty model</param>
        /// <response code="204">If the item updated</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
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

        /// <summary>
        /// Deletes faculty.
        /// </summary>
        /// <param name="id">Faculty id</param>
        /// <response code="204">If the item deleted</response>
        /// <response code="404">If the item not found</response>
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _facultyService.DeleteAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Create a statement.
        /// </summary>
        /// <param name="id">Faculty id</param>
        /// <response code="200">If faculty has enrollees</response>
        /// <response code="404">If thefaculty is empty</response>
        //[Authorize(Roles = "admin")]
        [Route("createFacultyStatement")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> TakeFacultiesEnrollees(int id)
        {
            var rank = await _enrolleeService.CalculateRatings();
            var enrollees = await _facultyService.GetFacultyEnrolleeIds(id);
            var enrolleeIds = _mapper.Map<IEnumerable<StatementModel>>(enrollees);
            return Ok(enrolleeIds);
        }
    }
}