using SelectionCommittee.DAL.Entities.Base;

namespace SelectionCommittee.DAL.Entities
{
    public class Assessment : Entity
    {
        public string Name { get; set; }
        public byte Grade { get; set; }
        public string GradeType { get; set; }
        public Enrollee Enrollee { get; set; }
        public int EnrolleeId { get; set; }
    }
}