using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SelectionCommittee.BLL.Assessments;

namespace SelectionCommittee.API.Services.Assessments
{
    public interface IAssessmentResponseComposer
    {
        /// <summary>
        /// Composes response code for GetAll action in AssessmentsConroller.
        /// </summary>
        /// <param name="assessmentDtos">Assessments DTOs</param>
        /// <returns>Returns action result for GetAll action in AssessmentsController</returns>
        ActionResult ComposeForGetAll(IEnumerable<AssessmentDto> assessmentDtos);

        /// <summary>
        /// Composes response code for Get action in AssessmentsControlle.
        /// </summary>
        /// <param name="assessmentDto">Assessment DTO</param>
        /// <returns>Returns action result for Get action in AssessmentsController</returns>
        ActionResult ComposeForGet(AssessmentDto assessmentDto);

        /// <summary>
        /// Composes response code for Add action in AssessmentsControler.
        /// </summary>
        /// <param name="statusCode">Code representing status of assessment addition operation</param>
        /// <param name="assessmentCreateDto">Assessment DTO</param>
        /// <returns>Returns action result for Add action in AssessmentsController</returns>
        ActionResult ComposeForCreate(int statusCode, AssessmentCreateDto assessmentCreateDto);

        /// <summary>
        /// Compose repsponse code for Update action in AssessmentsController.
        /// </summary>
        /// <param name="statusCode">Code representing status of the assessment updating operation</param>
        /// <returns>Returns action result for Update action in Assessmentsontroller</returns>
        ActionResult ComposeForUpdate(int statusCode);

        /// <summary>
        /// Composes response code for Delete action in AssessmentsController.
        /// </summary>
        /// <param name="statusCode">Code representing status of deleting operation</param>
        /// <returns>Returns action result for Delete action in AssessmentsController</returns>
        ActionResult ComposeForDelete(int statusCode);

        /// <summary>
        /// Compose response code for GetAssessmentsForEnrollee action in AssessmentsController.
        /// </summary>
        /// <param name="assessmentDtos">Code representing status of getting operation</param>
        /// <returns>Returns action result for GetAssessmentsForEnrollee action in AssessmentsController</returns>
        ActionResult ComposeForGetAssessmentsForEnrollee(IEnumerable<AssessmentDto> assessmentDtos);
    }
}