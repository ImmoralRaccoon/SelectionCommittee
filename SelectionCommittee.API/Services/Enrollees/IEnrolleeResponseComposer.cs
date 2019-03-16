using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SelectionCommittee.BLL.Enrollees;

namespace SelectionCommittee.API.Services.Enrollees
{
    public interface IEnrolleeResponseComposer
    {
        ActionResult ComposeForGetAll(IEnumerable<EnrolleDto> enrolleDtos);

        ActionResult ComposeForGet(EnrolleDto enrolleDto);

        ActionResult ComposeForCreate(int statusCode, EnrolleCreateDto enrolleCreateDto);

        ActionResult ComposeForDecouplingTable(int statusCode);

        ActionResult ComposeForUpdate(int statusCode);

        ActionResult ComposeForUpdateStatus(int statusCode);

        ActionResult ComposeForDelete(int statusCode);
    }
}