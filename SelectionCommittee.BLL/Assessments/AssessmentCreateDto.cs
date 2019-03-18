namespace SelectionCommittee.BLL.Assessments
{
    public class AssessmentCreateDto
    {
        public int EnrolleId { get; set; }

        public string Name { get; set; }
        public byte Grade { get; set; }
        public string GradeType { get; set; }
    }
}