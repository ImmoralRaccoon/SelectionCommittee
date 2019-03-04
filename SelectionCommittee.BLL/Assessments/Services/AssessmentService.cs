using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SelectionCommittee.DAL.Entities;
using SelectionCommittee.DAL.UnitOfWork;

namespace SelectionCommittee.BLL.Assessments.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IUnitOfWork _selectionCommitteeDataStorage;
        private readonly IMapper _mapper;
        private readonly IAssessmentCreator _assessmentCreator;

        public AssessmentService(IUnitOfWork selectionCommitteeDataStorage, IMapper mapper, IAssessmentCreator assessmentCreator)
        {
            _selectionCommitteeDataStorage = selectionCommitteeDataStorage;
            _mapper = mapper;
            _assessmentCreator = assessmentCreator;
        }

        public async Task<IEnumerable<AssessmentDto>> GetAllAsync()
        {
            var assessments = await _selectionCommitteeDataStorage.AssessmentRepository.GetAll().ToListAsync();
            var assessmentsDtos = _mapper.Map<IEnumerable<AssessmentDto>>(assessments);
            return assessmentsDtos;
        }

        public async Task<AssessmentDto> GetAsync(int id)
        {
            var assessmnet = await _selectionCommitteeDataStorage.AssessmentRepository.GetAsync(id);
            var assessmentDto = _mapper.Map<AssessmentDto>(assessmnet);
            return assessmentDto;
        }

        public async Task<int> AddAsync(AssessmentCreateDto assessmentCreateDto)
        {
            var enrollee = await _selectionCommitteeDataStorage.EnrolleeRepository.GetAsync(assessmentCreateDto.EnrolleeId);

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
            return assessment.EnrolleeId;
        }

        public async Task<int> UpdateAsync(AssessmentUpdateDto assessmentUpdateDto)
        {
            var assessment = await _selectionCommitteeDataStorage.AssessmentRepository.GetAsync(assessmentUpdateDto.Id);

            assessment.Name = assessmentUpdateDto.Name;
            assessment.Grade = assessmentUpdateDto.Grade;
            assessment.GradeType = assessmentUpdateDto.GradeType;

            _selectionCommitteeDataStorage.AssessmentRepository.Update(assessment);
            await _selectionCommitteeDataStorage.SaveChangesAsync();
            return assessment.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            _selectionCommitteeDataStorage.AssessmentRepository.Delete(id);
            await _selectionCommitteeDataStorage.SaveChangesAsync();
            return 1;
        }
    }
}