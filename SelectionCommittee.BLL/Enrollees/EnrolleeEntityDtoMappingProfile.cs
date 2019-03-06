using AutoMapper;
using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.BLL.Enrollees
{
    public class EnrolleeEntityDtoMappingProfile  : Profile
    {
        public EnrolleeEntityDtoMappingProfile()
        {
            CreateMap<Enrollee, EnrolleDto>();
            CreateMap<EnrolleDto, Enrollee>();
            CreateMap<EnrolleCreateDto, Enrollee>();
            CreateMap<FacultyEnrolleeCreateDto, FacultyEnrollee>();
        }
    }
}