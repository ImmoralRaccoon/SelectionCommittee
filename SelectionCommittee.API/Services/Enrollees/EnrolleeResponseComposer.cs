using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelectionCommittee.API.Models.Enrollees;
using SelectionCommittee.BLL.Enrollees;

namespace SelectionCommittee.API.Services.Enrollees
{
    public class EnrolleeResponseComposer : IEnrolleeResponseComposer
    {
        private readonly IMapper _mapper;

        public EnrolleeResponseComposer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ActionResult ComposeForGetAll(IEnumerable<EnrolleDto> enrolleDtos)
        {
            var enrolleeModels = _mapper.Map<IEnumerable<EnrolleeModel>>(enrolleDtos);
            return new OkObjectResult(enrolleeModels);
        }

        public ActionResult ComposeForGet(EnrolleDto enrolleDto)
        {
            var enrolleeModel = _mapper.Map<EnrolleeModel>(enrolleDto);
            return new OkObjectResult(enrolleeModel);
        }

        public ActionResult ComposeForCreate(int statusCode, EnrolleCreateDto enrolleCreateDto)
        {
            switch (statusCode)
            {
                case -1:
                    return new BadRequestObjectResult("Invalid first name.");
                case -2:
                    return new BadRequestObjectResult("Invalid last name.");
                case -3:
                    return new BadRequestObjectResult("Invalid patronymic.");
                case -4:
                    return new BadRequestObjectResult("Invalid email.");
                case -5:
                    return new BadRequestObjectResult("Invalid city.");
                case -6:
                    return new BadRequestObjectResult("Invalid region.");
                case -7:
                    return new BadRequestObjectResult("Invalid school or lyceum name.");
                default:
                    return new CreatedAtRouteResult("GetEnrollee", new { Id = statusCode }, enrolleCreateDto);
            }
        }

        public ActionResult ComposeForDecouplingTable(int statusCode)
        {
            switch (statusCode)
            {
                case -8:
                    return new BadRequestObjectResult("Invalid enrollee id.");
                case -9:
                    return new BadRequestObjectResult("Invalid faculty id.");
                default:
                    return new OkResult();
            }
        }

        public ActionResult ComposeForUpdate(int statusCode)
        {
            switch (statusCode)
            {
                case -12:
                    return new BadRequestObjectResult("Invalid enrollee id.");
                case -4:
                    return new BadRequestObjectResult("Invalid email.");
                case -5:
                    return new BadRequestObjectResult("Invalid city.");
                case -6:
                    return new BadRequestObjectResult("Invalid region.");
                case -7:
                    return new BadRequestObjectResult("Invalid school or lyceum name.");
                default:
                    return new OkResult();
            }
        }

        public ActionResult ComposeForUpdateStatus(int statusCode)
        {
            switch (statusCode)
            {
                case -13:
                    return new BadRequestObjectResult("Invalid enrollee id.");
                case -10:
                    return new BadRequestObjectResult("Invalid status.");
                default:
                    return new OkResult();
            }
        }

        public ActionResult ComposeForDelete(int statusCode)
        {
            switch (statusCode)
            {
                case -11:
                    return new NotFoundResult();
                default:
                    return new NoContentResult();
            }
        }
    }
}