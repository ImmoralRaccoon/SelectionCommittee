using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SelectionCommittee.DAL.EF;
using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.DAL.Repositories.Assessments
{
    public class AssessmentRepository : Repository<Assessment>, IAssessmentRepository
    {
        private readonly DbSet<Assessment> _assessments;

        public AssessmentRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _assessments = applicationDbContext.Assessments;
        }

        public override async Task<Assessment> GetAsync(int id)
        {
            return await _assessments
                .Include(a => a.Enrollee)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public override IQueryable<Assessment> GetAll()
        {
            return _assessments
                .Include(a => a.Enrollee);
        }

        public override void Delete(int id)
        {
            var assessment = new Assessment { Id = id };
            _assessments.Remove(assessment);
        }

        public override async Task AddAsync(Assessment entity)
        {
            await _assessments.AddAsync(entity);
        }

        public override void Update(Assessment entity)
        {
            _assessments.Update(entity);
        }

        public override async Task<bool> ContainsEntityWithId(int id)
        {
            return await _assessments.AnyAsync(a => a.Id == id);
        }
    }
}