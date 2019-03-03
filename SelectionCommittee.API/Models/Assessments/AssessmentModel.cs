namespace SelectionCommittee.API.Models.Assessments
{
    public class AssessmentModel
    {
        public int Id { get; set; }
        public EnrolleeInfoModel EnrolleeInfoModel { get; set; }
        public string Name { get; set; }
        public byte Grade { get; set; }
        public string GradeType { get; set; }
    }
}