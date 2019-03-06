using System;
using System.Collections.Generic;
using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.DAL.Repositories.FacultyEnrollees
{
    public interface IFacultyEnrolleeRepository : IDisposable
    {
        void Delete(IEnumerable<FacultyEnrollee> facultyEnrollees);
    }
}