using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SelectionCommittee.DAL.Entities;
using SelectionCommittee.DAL.UnitOfWork;
using SelectionCommittee.Logger;

namespace SelectionCommittee.BLL.Assessments.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IUnitOfWork _selectionCommitteeDataStorage;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public AssessmentService(IUnitOfWork selectionCommitteeDataStorage, IMapper mapper, ILoggerManager logger)
        {
            _selectionCommitteeDataStorage = selectionCommitteeDataStorage;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<AssessmentDto>> GetAllAsync()
        {
            var assessments = await _selectionCommitteeDataStorage.AssessmentRepository.GetAll().ToListAsync();
            var assessmentsDtos = _mapper.Map<IEnumerable<AssessmentDto>>(assessments);

            _logger.LogInfo("GetAllAsync() method from SelectionCommittee.BLL.Assessments.Services.AssessmentService has been finished.");
            return assessmentsDtos;
        }

        public async Task<AssessmentDto> GetAsync(int id)
        {
            if (!await _selectionCommitteeDataStorage.AssessmentRepository.ContainsEntityWithId(id))
            {
                _logger.LogWarn("Invalid assessment id.");
                return null;
            }

            var assessmnet = await _selectionCommitteeDataStorage.AssessmentRepository.GetAsync(id);
            var assessmentDto = _mapper.Map<AssessmentDto>(assessmnet);

            _logger.LogInfo("GetAsync(int id) method from SelectionCommittee.BLL.Assessments.Services.AssessmentService has been finished.");
            return assessmentDto;
        }

        public async Task<int> AddAsync(AssessmentCreateDto assessmentCreateDto)
        {
            if (!await _selectionCommitteeDataStorage.EnrolleeRepository.ContainsEntityWithId(assessmentCreateDto.EnrolleId))
            {
                _logger.LogWarn("Incorrect AssessmentCreateModel. Create operation failed.");
                return -1;
            }
            if (string.IsNullOrEmpty(assessmentCreateDto.Name))
            {
                _logger.LogWarn("Incorrect AssessmentCreateModel. Create operation failed.");
                return -2;
            }
            if (string.IsNullOrEmpty(assessmentCreateDto.GradeType))
            {
                _logger.LogWarn("Incorrect AssessmentCreateModel. Create operation failed.");
                return -3;
            }
            if (assessmentCreateDto.Grade == 0)
            {
                _logger.LogWarn("Incorrect AssessmentCreateModel. Create operation failed.");
                return -4;
            }
            if (assessmentCreateDto.Grade > 12)
            {
                _logger.LogWarn("Incorrect AssessmentCreateModel. Create operation failed.");
                return -6;
            }

            var enrollee = await _selectionCommitteeDataStorage.EnrolleeRepository.GetAsync(assessmentCreateDto.EnrolleId);

            var assessment = new Assessment
            {
                EnrolleeId = enrollee.Id,
                Enrollee = enrollee,

                Name = assessmentCreateDto.Name,
                Grade = assessmentCreateDto.Grade,
                GradeType = assessmentCreateDto.GradeType
            };

            await _selectionCommitteeDataStorage.AssessmentRepository.AddAsync(assessment);
            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("AddAsync(AssessmentCreateDto assessmentCreateDto) method from SelectionCommittee.BLL.Assessments.Services.AssessmentService has been finished.");
            return assessment.EnrolleeId;
        }

        public async Task<int> UpdateAsync(AssessmentUpdateDto assessmentUpdateDto)
        {
            if (assessmentUpdateDto.EnrolleId == 0)
            {
                _logger.LogWarn("Incorrect AssessmentUpdateModel. Update operation failed.");
                return -1;
            }
            if (string.IsNullOrEmpty(assessmentUpdateDto.Name))
            {
                _logger.LogWarn("Incorrect AssessmentUpdateModel. Update operation failed.");
                return -2;
            }
            if (string.IsNullOrEmpty(assessmentUpdateDto.GradeType))
            {
                _logger.LogWarn("Incorrect AssessmentUpdateModel. Update operation failed.");
                return -3;
            }
            if (assessmentUpdateDto.Grade == 0)
            {
                _logger.LogWarn("Incorrect AssessmentUpdateModel. Update operation failed.");
                return -4;
            }
            if (assessmentUpdateDto.Grade > 12)
            {
                _logger.LogWarn("Incorrect AssessmentUpdateModel. Update operation failed.");
                return -6;
            }

            var assessment = await _selectionCommitteeDataStorage.AssessmentRepository.GetAsync(assessmentUpdateDto.Id);

            assessment.Name = assessmentUpdateDto.Name;
            assessment.Grade = assessmentUpdateDto.Grade;
            assessment.GradeType = assessmentUpdateDto.GradeType;

            _selectionCommitteeDataStorage.AssessmentRepository.Update(assessment);
            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("UpdateAsync(AssessmentUpdateDto assessmentUpdateDto) method from SelectionCommittee.BLL.Assessments.Services.AssessmentService has been finished.");
            return assessment.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            if (!await _selectionCommitteeDataStorage.AssessmentRepository.ContainsEntityWithId(id))
            {
                _logger.LogWarn("Invalid assessment id. Delete operation failed.");
                return -5;
            }

            _selectionCommitteeDataStorage.AssessmentRepository.Delete(id);
            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("DeleteAsync(int id) method from SelectionCommittee.BLL.Assessments.Services.AssessmentService has been finished.");
            return 1;
        }

        public async Task<IEnumerable<AssessmentDto>> GetAllAssessmentsForEnrollee(int id)
        {
            if (!await _selectionCommitteeDataStorage.EnrolleeRepository.ContainsEntityWithId(id))
            {
                _logger.LogError("Enrollees table is empty. Unable to get assessment for uncreated enrollee.");
                return null;
            }

            var enrolleesAssessment = await _selectionCommitteeDataStorage.AssessmentRepository.GetAll()
                .Where(a => a.EnrolleeId == id).ToListAsync();
            var assessmentDto = _mapper.Map<IEnumerable<AssessmentDto>>(enrolleesAssessment);

            _logger.LogInfo("GetAllAssessmentsForEnrollee(int id) method from SelectionCommittee.BLL.Assessments.Services.AssessmentService has been finished.");
            return assessmentDto;
        }
    }
}