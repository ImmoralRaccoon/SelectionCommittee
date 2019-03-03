using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SelectionCommittee.DAL.EF;
using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.API.Controllers
{
    [Route("api/[controller]")]
    public class EnrolleeController : Controller
    {
        private ApplicationDbContext _context;

        public EnrolleeController(ApplicationDbContext context)
        {
            _context = context;

            //if (!_context.Enrollees.Any())
            //{
            //    _context.Enrollees.Add(new Enrollee { FirstName = "Lesha" });
            //    _context.Enrollees.Add(new Enrollee { FirstName = "Vlad" });
            //    _context.SaveChanges();
            //}
        }

        [HttpGet]
        public IEnumerable<Enrollee> Get()
        {
            return _context.Enrollees.ToList();
        }
    }
}