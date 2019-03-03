namespace SelectionCommittee.API.Models.Assessments
{
    public class AssessmentAddOrUpdateModel
    {
        public int EnrolleeId { get; set; }

        public string Name { get; set; }
        public byte Grade { get; set; }
        public string GradeType { get; set; }
    }
}