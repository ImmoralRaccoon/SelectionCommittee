using System.Collections.Generic;
using SelectionCommittee.DAL.Entities.Base;

namespace SelectionCommittee.DAL.Entities
{
    public class Enrollee : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string SchoolLyceumName { get; set; }
        public ICollection<FacultyEnrollee> FacultyEnrolles { get; set; }
        public ICollection<Assessment> Assessments { get; set; }
    }
}