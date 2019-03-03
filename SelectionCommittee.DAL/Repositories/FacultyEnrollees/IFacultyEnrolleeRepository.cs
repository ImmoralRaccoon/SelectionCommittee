using System;
using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.DAL.Repositories.FacultyEnrollees
{
    public interface IFacultyEnrolleeRepository : IDisposable
    {
        void Delete(int id);
    }
}