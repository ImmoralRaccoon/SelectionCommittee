namespace SelectionCommittee.API.Models
{
    public class StatementModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public double Rating { get; set; }
    }
}