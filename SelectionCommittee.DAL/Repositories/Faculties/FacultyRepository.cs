using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SelectionCommittee.DAL.EF;
using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.DAL.Repositories.Faculties
{
    public class FacultyRepository : Repository<Faculty>, IFacultyRepository
    {
        private readonly DbSet<Faculty> _faculties;

        public FacultyRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _faculties = DbContext.Faculties;
        }

        public override IQueryable<Faculty> GetAll()
        {
            return _faculties
                .Include(f => f.FacultyEnrolles)
                .ThenInclude(fe => fe.Enrollee);
        }

        public override async Task<Faculty> GetAsync(int id)
        {
            return await _faculties
                .Include(f => f.FacultyEnrolles)
                .ThenInclude(fe=>fe.Enrollee)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public override void Delete(int id)
        {
            var faculty = new Faculty { Id = id };
            _faculties.Remove(faculty);
        }

        public override async Task AddAsync(Faculty entity)
        {
            await _faculties.AddAsync(entity);
        }

        public override void Update(Faculty entity)
        {
            _faculties.Update(entity);
        }

        public override async Task<bool> ContainsEntityWithId(int id)
        {
            return await _faculties.AnyAsync(f => f.Id == id);
        }
    }
}