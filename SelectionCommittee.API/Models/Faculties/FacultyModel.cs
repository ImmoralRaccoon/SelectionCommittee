namespace SelectionCommittee.API.Models.Faculties
{
    public class FacultyModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public byte NumberOfBudgetPlaces { get; set; }
        public byte NumberOfPlaces { get; set; }
    }
}