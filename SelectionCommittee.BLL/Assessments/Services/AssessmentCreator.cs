using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.BLL.Assessments.Services
{
    public class AssessmentCreator : IAssessmentCreator
    {
        public Assessment CreateAssessmentForEddition(AssessmentCreateDto assessmentCreateDto, Enrollee enrollee)
        {
            var assessment = new Assessment
            {
                EnrolleeId = enrollee.Id,
                Enrollee = enrollee,

                Name = assessmentCreateDto.Name,
                Grade = assessmentCreateDto.Grade,
                GradeType = assessmentCreateDto.GradeType
            };

            return assessment;
        }
    }
}