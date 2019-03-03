using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelectionCommittee.BLL.Assessments.Services
{
    public interface IAssessmentService
    {
        Task<IEnumerable<AssessmentDto>> GetAllAsync();

        Task<AssessmentDto> GetAsync(int id);

        Task<int> AddAsync(AssessmentCreateDto assessmentCreateDto);

        Task<int> UpdateAsync(AssessmentUpdateDto assessmentUpdateDto);

        Task<int> DeleteAsync(int id);
    }
}