using AutoMapper;
using SelectionCommittee.API.Models.Faculties;
using SelectionCommittee.BLL.Faculties;

namespace SelectionCommittee.API.MappingConfiguration
{
    public class FacultyDtoModelMappingProfile : Profile
    {
        public FacultyDtoModelMappingProfile()
        {
            CreateMap<FacultyDto, FacultyModel>();
            CreateMap<FacultyAddOrUpdateModel, FacultyCreateDto>();
            CreateMap<FacultyAddOrUpdateModel, FacultyUpdateDto>();
        }
    }
}