using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SelectionCommittee.API.Models.Assessments;
using SelectionCommittee.API.Services.Assessments;
using SelectionCommittee.BLL.Assessments;
using SelectionCommittee.BLL.Assessments.Services;

namespace SelectionCommittee.API.Controllers
{
    [Authorize(Roles = "user")]
    [Route("api/[controller]")]
    public class AssessmentsController : Controller
    {
        private readonly IAssessmentService _assessmentService;
        private readonly IMapper _mapper;
        private readonly IAssessmentResponseComposer _assessmentResponseComposer;

        public AssessmentsController(IAssessmentService assessmentService, IMapper mapper, IAssessmentResponseComposer assessmentResponseComposer)
        {
            _assessmentService = assessmentService;
            _mapper = mapper;
            _assessmentResponseComposer = assessmentResponseComposer;
        }

        /// <summary>
        /// Get all assessments.
        /// </summary>
        /// <returns>Returns all assessments</returns>
        /// <response code="200">Always</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllAsync()
        {
            var assessmentDto = await _assessmentService.GetAllAsync();
            var response = _assessmentResponseComposer.ComposeForGetAll(assessmentDto);
            return response;
        }

        /// <summary>
        /// Get assessment by id.
        /// </summary>
        /// <param name="id">Assessment id</param>
        /// <returns>Returns assessment by id</returns>
        /// <response code="200">Always</response>
        /// <response code="404">If the item is not found</response>
        [HttpGet("{id}", Name = "GetAssessment")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var assessmentDto = await _assessmentService.GetAsync(id);
            var response = _assessmentResponseComposer.ComposeForGet(assessmentDto);
            return response;
        }

        /// <summary>
        /// Creates an assessment.
        /// </summary>
        /// <param name="assessmentAddOrUpdateModel">Assessment model</param>
        /// <returns>Returns route to created assessment</returns>
        /// <response code="201">If the item created</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddAsync([FromBody] AssessmentAddOrUpdateModel assessmentAddOrUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var assessmentCreateDto = _mapper.Map<AssessmentCreateDto>(assessmentAddOrUpdateModel);
            var statusCode = await _assessmentService.AddAsync(assessmentCreateDto);
            var response = _assessmentResponseComposer.ComposeForCreate(statusCode, assessmentCreateDto);
            return response;
        }

        /// <summary>
        /// Updates assessment.
        /// </summary>
        /// <param name="id">Assessment id</param>
        /// <param name="assessmentAddOrUpdateModel">Assessment model</param>
        /// <response code="204">If the item updated</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateAsync(int? id, [FromBody]AssessmentAddOrUpdateModel assessmentAddOrUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (id.HasValue)
            {
                var assessmentUpdateDto = _mapper.Map<AssessmentUpdateDto>(assessmentAddOrUpdateModel);
                assessmentUpdateDto.Id = id.Value;
                var statusCode = await _assessmentService.UpdateAsync(assessmentUpdateDto);
                var response = _assessmentResponseComposer.ComposeForUpdate(statusCode);
                return response;
            }
            else
            {
                var assessmentCreateDto = _mapper.Map<AssessmentCreateDto>(assessmentAddOrUpdateModel);
                var assessmentCreateModel = await _assessmentService.AddAsync(assessmentCreateDto);
                return Ok(assessmentCreateModel);
            }
        }

        /// <summary>
        /// Deletes assessment.
        /// </summary>
        /// <param name="id">Assessment id</param>
        /// <response code="204">If the item deleted</response>
        /// <response code="404">If the item not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var statusCode = await _assessmentService.DeleteAsync(id);
            var response = _assessmentResponseComposer.ComposeForDelete(statusCode);
            return response;
        }
    }
}