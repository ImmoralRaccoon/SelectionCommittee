using System;
using System.Threading.Tasks;
using SelectionCommittee.DAL.EF;
using SelectionCommittee.DAL.Repositories.Assessments;
using SelectionCommittee.DAL.Repositories.Enrollees;
using SelectionCommittee.DAL.Repositories.Faculties;

namespace SelectionCommittee.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private bool _disposed;

        public UnitOfWork(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IAssessmentRepository AssessmentRepository
        {
            get
            {
                if (_assessmentRepository == null)
                {
                    _assessmentRepository = new AssessmentRepository(_dbContext);
                }
                return _assessmentRepository;
            }
        }
        private IAssessmentRepository _assessmentRepository;

        public IEnrolleeRepository EnrolleeRepository
        {
            get
            {
                if (_enrolleeRepository == null)
                {
                    _enrolleeRepository = new EnrolleeRepository(_dbContext);
                }
                return _enrolleeRepository;
            }
        }
        private IEnrolleeRepository _enrolleeRepository;

        public IFacultyRepository FacultyRepository
        {
            get
            {
                if (_facultyRepository == null)
                {
                    _facultyRepository = new FacultyRepository(_dbContext);
                }
                return _facultyRepository;
            }
        }
        private IFacultyRepository _facultyRepository;

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                AssessmentRepository?.Dispose();
                EnrolleeRepository?.Dispose();
                FacultyRepository?.Dispose();

                _dbContext?.Dispose();
            }

            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}