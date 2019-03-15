using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SelectionCommittee.BLL.Assessments;

namespace SelectionCommittee.API.Services.Assessments
{
    public interface IAssessmentResponseComposer
    {
        ActionResult ComposeForGetAll(IEnumerable<AssessmentDto> assessmentDtos);

        ActionResult ComposeForGet(AssessmentDto assessmentDto);

        ActionResult ComposeForCreate(int statusCode, AssessmentCreateDto assessmentCreateDto);

        ActionResult ComposeForUpdate(int statusCode);

        ActionResult ComposeForDelete(int statusCode);
    }
}