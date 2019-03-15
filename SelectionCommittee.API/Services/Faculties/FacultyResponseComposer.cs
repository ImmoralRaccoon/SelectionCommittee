using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelectionCommittee.API.Models;
using SelectionCommittee.API.Models.Faculties;
using SelectionCommittee.BLL.Faculties;
using SelectionCommittee.BLL.Faculties.Services;

namespace SelectionCommittee.API.Services.Faculties
{
    public class FacultyResponseComposer : IFacultyResponseComposer
    {
        private readonly IMapper _mapper;

        public FacultyResponseComposer(IMapper mapper)
        {
            _mapper = mapper;
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
                default:
                    return new CreatedAtRouteResult("Get faculty", new { Id = statusCode }, facultyCreateDto);
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
            var statementModel = _mapper.Map<IEnumerable<StatementModel>>(statementDtos);
            return new OkObjectResult(statementModel);
        }
    }
}