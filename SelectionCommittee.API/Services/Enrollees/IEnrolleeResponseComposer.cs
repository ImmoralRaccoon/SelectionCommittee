using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SelectionCommittee.BLL.Enrollees;

namespace SelectionCommittee.API.Services.Enrollees
{
    public interface IEnrolleeResponseComposer
    {
        /// <summary>
        /// Composes response code for GetAll action in EnrolleesController.
        /// </summary>
        /// <param name="enrolleDtos">Enrollee DTOs</param>
        /// <returns>Returns action result for GetAll action in EnrolleesControlle</returns>
        ActionResult ComposeForGetAll(IEnumerable<EnrolleDto> enrolleDtos);

        /// <summary>
        /// Composes response code for Get action in EnrolleesController.
        /// </summary>
        /// <param name="enrolleDto">Enrollee DTO</param>
        /// <returns>Returns action result for Get action in EnrolleesController</returns>
        ActionResult ComposeForGet(EnrolleDto enrolleDto);

        /// <summary>
        /// Composes response code for Add action in EnrolleesController.
        /// </summary>
        /// <param name="statusCode">Code representing status of enrollee addition operation</param>
        /// <param name="enrolleCreateDto">Enrollee DTO for creation</param>
        /// <returns></returns>
        ActionResult ComposeForCreate(int statusCode, EnrolleCreateDto enrolleCreateDto);

        /// <summary>
        /// Composes response code for AddFacultyToEnrollee action in EnrolleesController.
        /// </summary>
        /// <param name="statusCode">Code representing status of enrollee addition faculty operation</param>
        /// <returns>Returns action result for AddFacultyToEnrollee action in EnrolleesController</returns>
        ActionResult ComposeForDecouplingTable(int statusCode);

        /// <summary>
        /// Composes response code for Update action in EnrolleesController.
        /// </summary>
        /// <param name="statusCode">Code representing status of enrollee updating operation</param>
        /// <returns>Returns action result for Update action in EnrolleesController</returns>
        ActionResult ComposeForUpdate(int statusCode);

        /// <summary>
        /// Composes response code for UpdateStatus action in EnrolleesController.
        /// </summary>
        /// <param name="statusCode">Code representing status of enrollee updating status operation</param>
        /// <returns>Returns action result for UpdateStatus action in EnrolleesController</returns>
        ActionResult ComposeForUpdateStatus(int statusCode);

        /// <summary>
        /// Composes response code for Delete action in EnrolleesController.
        /// </summary>
        /// <param name="statusCode">Code representing status of enrollee deleting operation</param>
        /// <returns>Returns action result for Delete action in EnrolleesController</returns>
        ActionResult ComposeForDelete(int statusCode);
    }
}