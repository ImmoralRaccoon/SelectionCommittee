using System;
using System.Threading.Tasks;
using SelectionCommittee.DAL.EF;
using SelectionCommittee.DAL.Repositories.Assessments;
using SelectionCommittee.DAL.Repositories.Enrollees;
using SelectionCommittee.DAL.Repositories.Faculties;
using SelectionCommittee.DAL.Repositories.FacultyEnrollees;

namespace SelectionCommittee.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private bool _disposed;

        private IAssessmentRepository _assessmentRepository;
        private IEnrolleeRepository _enrolleeRepository;
        private IFacultyRepository _facultyRepository;
        private IFacultyEnrolleeRepository _facultyEnrolleeRepository;

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

        public IFacultyEnrolleeRepository FacultyEnrolleeRepository
        {
            get
            {
                if (_facultyEnrolleeRepository == null)
                {
                    _facultyEnrolleeRepository = new FacultyEnrolleeRepository(_dbContext);
                }
                return _facultyEnrolleeRepository;
            }
        }

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
                FacultyEnrolleeRepository?.Dispose();

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