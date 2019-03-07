using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.DAL.Repositories.FacultyEnrollees
{
    public interface IFacultyEnrolleeRepository : IDisposable
    {
        Task<IEnumerable<FacultyEnrollee>> GetByFacultyId(int id);
        Task<IEnumerable<FacultyEnrollee>> GetByEnrolleeId(int id);
        Task AddAsync(FacultyEnrollee facultyEnrollee);
        Task RemoveRange(IEnumerable<FacultyEnrollee> facultyEnrollee);
    }
}