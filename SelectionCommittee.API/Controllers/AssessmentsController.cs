using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelectionCommittee.API.Models.Assessments;
using SelectionCommittee.BLL.Assessments.Services;

namespace SelectionCommittee.API.Controllers
{
    [Route("api/[controller]")]
    public class AssessmentsController : Controller
    {
        private readonly IAssessmentService _assessmentService;
        private readonly IMapper _mapper;

        public AssessmentsController(IAssessmentService assessmentService, IMapper mapper)
        {
            _assessmentService = assessmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var assessmentDto = await _assessmentService.GetAllAsync();
            var assessmentModels = _mapper.Map<IEnumerable<AssessmentModel>>(assessmentDto);
            return Ok(assessmentModels);
        }
    }
}