namespace SelectionCommittee.BLL.Assessments
{
    public class AssessmentUpdateDto
    {
        public int Id { get; set; }

        public int EnrolleId { get; set; }

        public string Name { get; set; }
        public byte Grade { get; set; }
        public string GradeType { get; set; }
    }
}