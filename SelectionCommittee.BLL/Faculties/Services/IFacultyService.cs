using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SelectionCommittee.BLL.Faculties.Services
{
    public interface IFacultyService
    {
        Task<IEnumerable<FacultyDto>> GetAllAsync();

        Task<IEnumerable<FacultyDto>> GetAllSortedByNameAsync();

        Task<IEnumerable<FacultyDto>> GetAllSortedByAmountOfPlacesAsync();

        Task<IEnumerable<FacultyDto>> GetAllSortedByAmountofBudgetPlacesAsync();

        Task<FacultyDto> GetAsync(int id);

        Task<int> AddAsync(FacultyCreateDto facultyCreateDto);

        Task<int> UpdateAsync(FacultyUpdateDto facultyUpdateDto);

        Task<int> DeleteAsync(int id);
    }
}