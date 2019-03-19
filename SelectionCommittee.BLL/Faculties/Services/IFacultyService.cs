using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelectionCommittee.BLL.Faculties.Services
{
    public interface IFacultyService
    {
        /// <summary>
        /// Gets all faculties.
        /// </summary>
        /// <returns>Returns all faculties</returns>
        Task<IEnumerable<FacultyDto>> GetAllAsync();

        /// <summary>
        /// Gets all faculties sorted by name(a-z).
        /// </summary>
        /// <returns>Returns all faculties sorted by name(a-z)</returns>
        Task<IEnumerable<FacultyDto>> GetAllSortedByNameFromAAsync();

        /// <summary>
        /// Gets all faculties sorted by name(z-a).
        /// </summary>
        /// <returns>Returns all faculties sorted by name(z-a)</returns>
        Task<IEnumerable<FacultyDto>> GetAllSortedByNameFromZAsync();

        /// <summary>
        /// Gets all faculties sorted by amount of places.
        /// </summary>
        /// <returns>Returns all faculties sorted by amount of places.</returns>
        Task<IEnumerable<FacultyDto>> GetAllSortedByAmountOfPlacesAsync();

        /// <summary>
        /// Gets all faculties sorted by amount of budget places.
        /// </summary>
        /// <returns>Returns all faculties sorted by amount of budget places.</returns>
        Task<IEnumerable<FacultyDto>> GetAllSortedByAmountofBudgetPlacesAsync();

        /// <summary>
        /// Gets faculty by id.
        /// </summary>
        /// <param name="id">Faculty id</param>
        /// <returns>Returns faculty by id</returns>
        Task<FacultyDto> GetAsync(int id);

        /// <summary>
        /// Creates faculty.
        /// </summary>
        /// <param name="facultyCreateDto">Faculty create model</param>
        /// <returns>Returns id of created entity or status code in case of failure</returns>
        Task<int> AddAsync(FacultyCreateDto facultyCreateDto);

        /// <summary>
        /// Updates facultyFaculty update model
        /// </summary>
        /// <param name="facultyUpdateDto">Faculty update model</param>
        /// <returns>Returns id of updated entity or status code in case of failure</returns>
        Task<int> UpdateAsync(FacultyUpdateDto facultyUpdateDto);

        /// <summary>
        /// Deletes faculty.
        /// </summary>
        /// <param name="id">Faculty id</param>
        /// <returns>Returns status code of operation result</returns>
        Task<int> DeleteAsync(int id);

        /// <summary>
        /// Gets all enrollees for faculty.
        /// </summary>
        /// <param name="id">Faculty id</param>
        /// <returns>Returns collection of enrollees or status code in case of failure</returns>
        Task<IEnumerable<StatementDto>> GetFacultyEnrollees(int id);
    }
}