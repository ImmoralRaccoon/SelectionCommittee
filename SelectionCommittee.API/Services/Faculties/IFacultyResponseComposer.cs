using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SelectionCommittee.BLL.Faculties;

namespace SelectionCommittee.API.Services.Faculties
{
    public interface IFacultyResponseComposer
    {
        /// <summary>
        /// Composes response code for GetAll action in FacultiesController.
        /// </summary>
        /// <param name="facultyDtos">Faculty DTOs</param>
        /// <returns>Retunrns action result for GetAll action in FacultiesController</returns>
        IActionResult ComposeForGetAll(IEnumerable<FacultyDto> facultyDtos);

        /// <summary>
        /// Composes response code for GetAllByNameFromA action in FacultiesController.
        /// </summary>
        /// <param name="facultyDtos">Faculty DTOs</param>
        /// <returns>Retunrns action result for GetAllByNameFromA action in FacultiesController</returns>
        IActionResult ComposeForGetAllByNameFromA(IEnumerable<FacultyDto> facultyDtos);

        /// <summary>
        /// Composes response code for GetAllByNameFromZ action in FacultiesController.
        /// </summary>
        /// <param name="facultyDtos">Faculty DTOs</param>
        /// <returns>Retunrns action result for GetAllByNameFromZ action in FacultiesController</returns>
        IActionResult ComposeForGetAllByNameFromZ(IEnumerable<FacultyDto> facultyDtos);

        /// <summary>
        /// Composes response code for GetAllByNumberOfPlaces action in FacultiesController.
        /// </summary>
        /// <param name="facultyDtos">Faculty DTOs</param>
        /// <returns>Retunrns action result for GetAllByumberOfPlaces action in FacultiesController</returns>
        IActionResult ComposeForGetAllByNumberOfPlaces(IEnumerable<FacultyDto> facultyDtos);

        /// <summary>
        /// Composes response code for GetAllByNumberOfBudgetPlaces action in FacultiesController.
        /// </summary>
        /// <param name="facultyDtos">Faculty DTOs</param>
        /// <returns>Retunrns action result for GetAllByNumberOfBudgetPlaces action in FacultiesController</returns>
        IActionResult ComposeForGetAllByNumberOfBudgetPlaces(IEnumerable<FacultyDto> facultyDtos);

        /// <summary>
        /// Composes response for Get action in FacultiesController.
        /// </summary>
        /// <param name="facultyDto">Faculty DTO</param>
        /// <returns>Retunrns action for Get in FacultiesController</returns>
        IActionResult ComposeForGet(FacultyDto facultyDto);

        /// <summary>
        /// Composes response code for Add action in FacultiesController.
        /// </summary>
        /// <param name="statusCode">Code representing status of faculty addition operation</param>
        /// <param name="facultyCreateDto">Faculty DTO for creation</param>
        /// <returns>Returns action result for Add action in FacultiesController</returns>
        IActionResult ComposeForCreate(int statusCode, FacultyCreateDto facultyCreateDto);

        /// <summary>
        /// Composes response for Update action in FacultiesController.
        /// </summary>
        /// <param name="statusCode">Code representing status of faculty updating operation</param>
        /// <returns>Returns action result for Update action in FacultiesController</returns>
        IActionResult ComposeForUpdate(int statusCode);

        /// <summary>
        /// Composes response for Delete action in FacultiesController.
        /// </summary>
        /// <param name="statusCode">Code representing status of faculty deleting operation</param>
        /// <returns>Returns action result for Delete action in FacultiesController</returns>
        IActionResult ComposeForDelete(int statusCode);

        /// <summary>
        /// Composes response for CreateStatement action in FacultiesController.
        /// </summary>
        /// <param name="facultyDtos">Code representing status of faculty creating statement operation</param>
        /// <returns>Returns action result for CreateStatement action in FacultiesController</returns>
        IActionResult ComposeForCreateStatement(IEnumerable<StatementDto> facultyDtos);
    }
}