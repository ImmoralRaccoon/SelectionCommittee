using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelectionCommittee.BLL.Faculties.Services
{
    public interface IFacultyService
    {
        Task<IEnumerable<FacultyDto>> GetAllAsync();

        Task<FacultyDto> GetAsync(int id);

        Task<int> AddAsync(FacultyCreateDto facultyCreateDto);

        Task<int> UpdateAsync(FacultyUpdateDto facultyUpdateDto);

        Task<int> DeleteAsync(int id);
    }
}