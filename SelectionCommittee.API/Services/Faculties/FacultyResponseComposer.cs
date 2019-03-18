using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelectionCommittee.API.Models;
using SelectionCommittee.API.Models.Faculties;
using SelectionCommittee.BLL.Faculties;
using SelectionCommittee.Logger;

namespace SelectionCommittee.API.Services.Faculties
{
    public class FacultyResponseComposer : IFacultyResponseComposer
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public FacultyResponseComposer(IMapper mapper, ILoggerManager logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult ComposeForGetAll(IEnumerable<FacultyDto> facultyDtos)
        {
            var facultyModels = _mapper.Map<IEnumerable<FacultyModel>>(facultyDtos);
            return new OkObjectResult(facultyModels);
        }

        public IActionResult ComposeForGetAllByNameFromA(IEnumerable<FacultyDto> facultyDtos)
        {
            var facultyModels = _mapper.Map<IEnumerable<FacultyModel>>(facultyDtos);
            return new OkObjectResult(facultyModels);
        }

        public IActionResult ComposeForGetAllByNameFromZ(IEnumerable<FacultyDto> facultyDtos)
        {
            var facultyModels = _mapper.Map<IEnumerable<FacultyModel>>(facultyDtos);
            return new OkObjectResult(facultyModels);
        }

        public IActionResult ComposeForGetAllByNumberOfPlaces(IEnumerable<FacultyDto> facultyDtos)
        {
            var facultyModels = _mapper.Map<IEnumerable<FacultyModel>>(facultyDtos);
            return new OkObjectResult(facultyModels);
        }

        public IActionResult ComposeForGetAllByNumberOfBudgetPlaces(IEnumerable<FacultyDto> facultyDtos)
        {
            var facultyModels = _mapper.Map<IEnumerable<FacultyModel>>(facultyDtos);
            return new OkObjectResult(facultyModels);
        }

        public IActionResult ComposeForGet(FacultyDto facultyDto)
        {
            if (facultyDto == null)
            {
                return new NotFoundResult();
            }

            var facultyModel = _mapper.Map<FacultyDto>(facultyDto);
            return new OkObjectResult(facultyModel);
        }

        public IActionResult ComposeForCreate(int statusCode, FacultyCreateDto facultyCreateDto)
        {
            switch (statusCode)
            {
                case -1:
                    return new BadRequestObjectResult("Invalid faculty name.");
                case -2:
                    return new BadRequestObjectResult("Invalid number of places.");
                case -3:
                    return new BadRequestObjectResult("Invalid number of budjet places.");
                case -6:
                    return new BadRequestObjectResult("Invalid faculty number of places (can`t be greater than 255).");
                case -7:
                    return new BadRequestObjectResult("Invalid faculty number of budget places (can`t be greater than 255).");
                default:
                    return new CreatedAtRouteResult("GetFaculty", new { Id = statusCode }, facultyCreateDto);
            }
        }

        public IActionResult ComposeForUpdate(int statusCode)
        {
            switch (statusCode)
            {
                case -1:
                    return new BadRequestObjectResult("Invalid faculty name.");
                case -2:
                    return new BadRequestObjectResult("Invalid number of places.");
                case -3:
                    return new BadRequestObjectResult("Invalid number of budjet places.");
                case -5:
                    return new NotFoundResult();
                case -6:
                    return new BadRequestObjectResult("Invalid faculty number of places (can`t be greater than 255).");
                case -7:
                    return new BadRequestObjectResult("Invalid faculty number of budget places (can`t be greater than 255).");
                default:
                    return new OkResult();
            }
        }

        public IActionResult ComposeForDelete(int statusCode)
        {
            switch (statusCode)
            {
                case -4:
                    return new NotFoundResult();
                default:
                    return new NoContentResult();
            }
        }

        public IActionResult ComposeForCreateStatement(IEnumerable<StatementDto> statementDtos)
        {
            if(statementDtos==null)
                return new BadRequestObjectResult("Invalid faculty id.");

            if (!statementDtos.Any())
            {
                _logger.LogWarn("Empty faculty. Operation failed.");
                return new BadRequestObjectResult("Faculty has no enrollees.");
            }

            var statementModel = _mapper.Map<IEnumerable<StatementModel>>(statementDtos);
            return new OkObjectResult(statementModel);
        }
    }
}