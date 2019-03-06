using System;
using System.Threading.Tasks;
using SelectionCommittee.DAL.Repositories.Assessments;
using SelectionCommittee.DAL.Repositories.Enrollees;
using SelectionCommittee.DAL.Repositories.Faculties;
using SelectionCommittee.DAL.Repositories.FacultyEnrollees;

namespace SelectionCommittee.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAssessmentRepository AssessmentRepository { get; }
        IEnrolleeRepository EnrolleeRepository { get; }
        IFacultyRepository FacultyRepository { get; }
        IFacultyEnrolleeRepository FacultyEnrolleeRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}