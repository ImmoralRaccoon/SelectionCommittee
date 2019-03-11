using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SelectionCommittee.DAL.EF;
using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.DAL.Repositories.Enrollees
{
    public class EnrolleeRepository : Repository<Enrollee>, IEnrolleeRepository
    {
        private readonly DbSet<Enrollee> _enrollees;

        public EnrolleeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _enrollees = DbContext.Enrollees;
        }

        public override IQueryable<Enrollee> GetAll()
        {
            return _enrollees
                .Include(e => e.Assessments)
                .Include(e => e.FacultyEnrolles);
        }

        public override async Task<Enrollee> GetAsync(int id)
        {
            return await _enrollees
                .Include(e => e.Assessments)
                .Include(e => e.FacultyEnrolles)
                    .ThenInclude(fe=>fe.Faculty)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public override async Task AddAsync(Enrollee entity)
        {
            await _enrollees.AddAsync(entity);
        }

        public override void Delete(int id)
        {
            var enrollee = new Enrollee { Id = id };
            _enrollees.Remove(enrollee);
        }

        public override void Update(Enrollee entity)
        {
            _enrollees.Update(entity);
        }

        public void UpdateStatus(Enrollee entity)
        {
            _enrollees.Update(entity);
        }

        public override async Task<bool> ContainsEntityWithId(int id)
        {
            return await _enrollees.AnyAsync(e => e.Id == id);
        }
    }
}