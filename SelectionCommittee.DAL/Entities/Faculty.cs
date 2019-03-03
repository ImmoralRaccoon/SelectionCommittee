using System.Collections.Generic;
using SelectionCommittee.DAL.Entities.Base;

namespace SelectionCommittee.DAL.Entities
{
    public class Faculty : Entity
    {
        public string Name { get; set; }
        public byte NumberOfBudgetPlaces { get; set; }
        public byte NumberOfPlaces { get; set; }
        public ICollection<FacultyEnrollee> FacultyEnrolles { get; set; }
    }
}