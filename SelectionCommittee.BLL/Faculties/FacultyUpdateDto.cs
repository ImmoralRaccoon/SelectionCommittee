namespace SelectionCommittee.BLL.Faculties
{
    public class FacultyUpdateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public byte NumberOfBudgetPlaces { get; set; }
        public byte NumberOfPlaces { get; set; }
    }
}