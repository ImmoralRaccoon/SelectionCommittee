using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelectionCommittee.BLL.Assessments.Services
{
    public interface IAssessmentService
    {
        /// <summary>
        /// Gets all assessments.
        /// </summary>
        /// <returns>Returns all assessments</returns>
        Task<IEnumerable<AssessmentDto>> GetAllAsync();

        /// <summary>
        /// Gets assessment by id.
        /// </summary>
        /// <param name="id">Assessment id</param>
        /// <returns>Returns assessment by id</returns>
        Task<AssessmentDto> GetAsync(int id);

        /// <summary>
        /// Creates assessment.
        /// </summary>
        /// <param name="assessmentCreateDto">Assessment creation model</param>
        /// <returns>Returns id of created entity or status code in case of failure</returns>
        Task<int> AddAsync(AssessmentCreateDto assessmentCreateDto);

        /// <summary>
        /// Updates assessment.
        /// </summary>
        /// <param name="assessmentUpdateDto">Assessment updating model</param>
        /// <returns>Returns id of updated entity or status code in case of failure</returns>
        Task<int> UpdateAsync(AssessmentUpdateDto assessmentUpdateDto);

        /// <summary>
        /// Deletes assessment.
        /// </summary>
        /// <param name="id">Assessmen id</param>
        /// <returns>Returns status code of operation result</returns>
        Task<int> DeleteAsync(int id);

        /// <summary>
        /// Gets all assessments for enrollee.
        /// </summary>
        /// <param name="id">Enrollee id</param>
        /// <returns>Return colection of assessments or status code in case of failure</returns>
        Task<IEnumerable<AssessmentDto>> GetAllAssessmentsForEnrollee(int id);
    }
}