using System;
using System.Threading.Tasks;
using SelectionCommittee.DAL.Repositories.Assessments;
using SelectionCommittee.DAL.Repositories.Enrollees;
using SelectionCommittee.DAL.Repositories.Faculties;

namespace SelectionCommittee.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAssessmentRepository AssessmentRepository { get; }
        IEnrolleeRepository EnrolleeRepository { get; }
        IFacultyRepository FacultyRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}