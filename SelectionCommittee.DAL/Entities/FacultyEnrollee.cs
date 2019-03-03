using SelectionCommittee.DAL.Entities.Base;

namespace SelectionCommittee.DAL.Entities
{
    public class FacultyEnrollee : Entity
    {
        public Enrollee Enrollee { get; set; }
        public int EnrolleeId { get; set; }
        public Faculty Faculty { get; set; }
        public int FacultyId { get; set; }
    }
}