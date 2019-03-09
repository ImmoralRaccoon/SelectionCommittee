using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SelectionCommittee.Authentication
{
    public class IdentityContext : IdentityDbContext<User>
    {
        public IdentityContext(DbContextOptions<IdentityContext> contextOptions) : base(contextOptions)
        {
            Database.EnsureCreated();
        }
    }
}