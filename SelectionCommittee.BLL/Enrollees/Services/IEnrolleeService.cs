using System.Collections.Generic;
using System.Threading.Tasks;
using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.BLL.Enrollees.Services
{
    public interface IEnrolleeService
    {
        /// <summary>
        /// Gets all enrollees.
        /// </summary>
        /// <returns>Returns all enrolles</returns>
        Task<IEnumerable<EnrolleDto>> GetAllAsync();

        /// <summary>
        /// Gets enrollee by id.
        /// </summary>
        /// <param name="id">Enrollee id</param>
        /// <returns>Returns enrollee by id</returns>
        Task<EnrolleDto> GetAsync(int id);

        /// <summary>
        /// Creates enrollee.
        /// </summary>
        /// <param name="enrolleCreateDto">Enrollee create model</param>
        /// <returns>Returns id of created entity or status code in case of failure</returns>
        Task<int> AddAsync(EnrolleCreateDto enrolleCreateDto);

        /// <summary>
        /// Adding enrollee to faculty.
        /// </summary>
        /// <param name="facultyEnrolleeCreateDto">Faculty enrollee create model</param>
        /// <returns>Returns 1 or status code in case of failure</returns>
        Task<int> AddFacultyEnrolleeAsync(FacultyEnrolleeCreateDto facultyEnrolleeCreateDto);

        /// <summary>
        /// Updates enrollee by id.
        /// </summary>
        /// <param name="enrolleeUpdateDto">Enrollee update model</param>
        /// <returns>Returns id of updated entity or status code in case of failure</returns>
        Task<int> UpdateAsync(EnrolleeUpdateDto enrolleeUpdateDto);

        /// <summary>
        /// Updates enrollee status.
        /// </summary>
        /// <param name="enrolleeUpdateStatusDto">Enrollee status update model</param>
        /// <returns>Returns id of updated entity or status code in case of failure</returns>
        Task<int> UpdateStatusAsync(EnrolleeUpdateStatusDto enrolleeUpdateStatusDto);

        /// <summary>
        /// Deletes enrollee.
        /// </summary>
        /// <param name="id">Enrollee id</param>
        /// <returns>Returns status code of operation result</returns>
        Task<int> DeleteAsync(int id);

        /// <summary>
        /// Calculate rating of enrollees.
        /// </summary>
        /// <returns>Return collection of sorted enrollees</returns>
        Task<IEnumerable<Enrollee>> CalculateRatings();

        /// <summary>
        /// Gets enrollee email by id.
        /// </summary>
        /// <param name="id">Enrollee id</param>
        /// <returns>Returns string email of enrollee</returns>
        Task<string> GetEnrolleEmail(int id);
    }
}