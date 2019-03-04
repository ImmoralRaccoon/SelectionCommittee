using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            throw new System.NotImplementedException();
        }

        public async Task<int> UpdateAsync(AssessmentUpdateDto assessmentUpdateDto)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}