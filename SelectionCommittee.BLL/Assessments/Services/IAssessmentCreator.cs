using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.BLL.Assessments.Services
{
    public interface IAssessmentCreator
    {
        Assessment CreateAssessmentForEddition(AssessmentCreateDto assessmentCreateDto,
            Enrollee enrollee);
    }
}