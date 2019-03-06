using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SelectionCommittee.DAL.EF;
using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.DAL.Repositories.FacultyEnrollees
{
    public class FacultyEnrolleeRepository : IFacultyEnrolleeRepository
    {
        private readonly DbSet<FacultyEnrollee> _facultyEnrollees;
        private readonly ApplicationDbContext _dbContext;
        private bool _disposed;

        public FacultyEnrolleeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _facultyEnrollees = dbContext.FacultyEnrollees;
        }

        public async Task<IEnumerable<FacultyEnrollee>> GetByFacultyId(int id)
        {
            return await _facultyEnrollees
                .Where(fe => fe.FacultyId == id).ToListAsync();
        }

        public async Task<IEnumerable<FacultyEnrollee>> GetByEnrolleeId(int id)
        {
            return await _facultyEnrollees
                .Where(fe => fe.EnrolleeId == id).ToListAsync();
        }

        public async Task AddAsync(FacultyEnrollee facultyEnrollee)
        {
            await _facultyEnrollees.AddAsync(facultyEnrollee);
        }

        public void RemoveRange(IEnumerable<FacultyEnrollee> facultyEnrollee)
        {
            _facultyEnrollees.RemoveRange(facultyEnrollee);
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
                _dbContext?.Dispose();
            }

            _disposed = true;
        }

        ~FacultyEnrolleeRepository()
        {
            Dispose(false);
        }
    }
}