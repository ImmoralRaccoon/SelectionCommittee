namespace SelectionCommittee.API.Models.Enrollees
{
    public class EnrolleeModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string SchoolLyceumName { get; set; }
        public string LockStatus { get; set; }
    }
}