using AutoMapper;
using SelectionCommittee.API.Models.Enrollees;
using SelectionCommittee.BLL.Enrollees;

namespace SelectionCommittee.API.MappingConfiguration
{
    public class EnrolleeDtoModelMappingProfile : Profile
    {
        public EnrolleeDtoModelMappingProfile()
        {
            CreateMap<EnrolleDto, EnrolleeModel>();
            CreateMap<EnrolleeAddModel, EnrolleCreateDto>();
            CreateMap<EnrolleeUpdateModel, EnrolleeUpdateDto>();
        }
    }
}