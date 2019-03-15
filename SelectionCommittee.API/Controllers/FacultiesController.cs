using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SelectionCommittee.API.Models;
using SelectionCommittee.API.Models.Faculties;
using SelectionCommittee.API.Services.Faculties;
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
        private readonly IFacultyResponseComposer _facultyResponseComposer;

        public FacultiesController(IFacultyService facultyService, IMapper mapper, IEnrolleeService enrolleeService, IFacultyResponseComposer facultyResponseComposer)
        {
            _facultyService = facultyService;
            _mapper = mapper;
            _enrolleeService = enrolleeService;
            _facultyResponseComposer = facultyResponseComposer;
        }

        /// <summary>
        /// Get all faculties.
        /// </summary>
        /// <returns>Returns all faculties</returns>
        /// <response code="200">Always</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllAsync()
        {
            var facultyDtos = await _facultyService.GetAllAsync();
            var response = _facultyResponseComposer.ComposeForGetAll(facultyDtos);
            return response;
        }

        /// <summary>
        /// Get all faculties sorted by name(a-z).
        /// </summary>
        /// <returns>Returns all faculties sorted by name(a-z)</returns>
        /// <response code="200">Always</response>
        [Route("byName(a-z)")]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllByNameFromAAsync()
        {
            var facultyDtos = await _facultyService.GetAllSortedByNameFromAAsync();
            var response = _facultyResponseComposer.ComposeForGetAllByNameFromA(facultyDtos);
            return response;
        }

        /// <summary>
        /// Get all faculties sorted by name(z-a).
        /// </summary>
        /// <returns>Returns all faculties sorted by name(a-z)</returns>
        /// <response code="200">Always</response>
        [Route("byName(z-a)")]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllByNameFromZAsync()
        {
            var facultyDtos = await _facultyService.GetAllSortedByNameFromZAsync();
            var response = _facultyResponseComposer.ComposeForGetAllByNameFromZ(facultyDtos);
            return response;
        }

        /// <summary>
        /// Get all faculties sorted by number of places.
        /// </summary>
        /// <returns>Returns all faculties sorted by number of places</returns>
        /// <response code="200">Always</response>
        [Route("byNumberOfPlaces")]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllByNumberOfPlacesAsync()
        {
            var facultyDtos = await _facultyService.GetAllSortedByAmountOfPlacesAsync();
            var response = _facultyResponseComposer.ComposeForGetAllByNumberOfPlaces(facultyDtos);
            return response;
        }

        /// <summary>
        /// Get all faculties sorted by number of budget places.
        /// </summary>
        /// <returns>Returns all faculties sorted by number of budget places</returns>
        /// <response code="200">Always</response>
        [Route("byNumberOfBudgetPlaces")]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllByNumberOfBudgetPlacesAsync()
        {
            var facultyDtos = await _facultyService.GetAllSortedByAmountofBudgetPlacesAsync();
            var response = _facultyResponseComposer.ComposeForGetAllByNumberOfBudgetPlaces(facultyDtos);
            return response;
        }

        /// <summary>
        /// Get faculty by id.
        /// </summary>
        /// <param name="id">Faculty id</param>
        /// <returns>Faculty by id</returns>
        /// <response code="200">If the item exists</response>
        /// <response code="404">If the item is not found</response>
        [HttpGet("{id}", Name = "GetFaculty")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var facultyDto = await _facultyService.GetAsync(id);
            var response = _facultyResponseComposer.ComposeForGet(facultyDto);
            return response;
        }

        /// <summary>
        /// Creates a faculty.
        /// </summary>
        /// <param name="facultyAddOrUpdateModel">Faculty model</param>
        /// <returns>Returns route to created faculty</returns>
        /// <response code="201">If the item created</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddAsync([FromBody] FacultyAddOrUpdateModel facultyAddOrUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            var facultyCreateDto = _mapper.Map<FacultyCreateDto>(facultyAddOrUpdateModel);
            var statusCode = await _facultyService.AddAsync(facultyCreateDto);
            //var response = _facultyResponseComposer.ComposeForCreate(statusCode, facultyCreateDto);
            return Ok(statusCode);
            //return response;
        }

        /// <summary>
        /// Uptades faculty.
        /// </summary>
        /// <param name="id">Faculty id</param>
        /// <param name="facultyAddOrUpdateModel">Faculty model</param>
        /// <response code="204">If the item updated</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        //[Authorize(Roles = "admin")]
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
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
                var statusCode = await _facultyService.UpdateAsync(facultyUpdateDto);
                var response = _facultyResponseComposer.ComposeForUpdate(statusCode);
                return response;
            }
            else
            {
                var facultyCreateDto = _mapper.Map<FacultyCreateDto>(facultyAddOrUpdateModel);
                var statusCode = await _facultyService.AddAsync(facultyCreateDto);
                var response = _facultyResponseComposer.ComposeForCreate(statusCode, facultyCreateDto);
                return response;
            }
        }

        /// <summary>
        /// Deletes faculty.
        /// </summary>
        /// <param name="id">Faculty id</param>
        /// <response code="204">If the item deleted</response>
        /// <response code="404">If the item not found</response>
        //[Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var statusCode = await _facultyService.DeleteAsync(id);
            var response = _facultyResponseComposer.ComposeForDelete(statusCode);
            return response;
        }

        /// <summary>
        /// Create a statement.
        /// </summary>
        /// <param name="id">Faculty id</param>
        /// <response code="200">If faculty has enrollees</response>
        /// <response code="404">If the faculty is empty</response>
        //[Authorize(Roles = "admin")]
        [Route("createFacultyStatement")]
        [HttpGet]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateStatementAsync(int id)
        {
            await _enrolleeService.CalculateRatings();
            var enrollees = await _facultyService.GetFacultyEnrollees(id);
            var response = _facultyResponseComposer.ComposeForCreateStatement(enrollees);
            return response;
        }
    }
}