using AutoMapper;
using SelectionCommittee.BLL.Faculties.Services;
using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.BLL.Faculties
{
    public class FacultyEntityDtoMappingProfile : Profile
    {
        public FacultyEntityDtoMappingProfile()
        {
            CreateMap<Faculty, FacultyDto>();
            CreateMap<FacultyDto, Faculty>();
            CreateMap<FacultyCreateDto, Faculty>();
            CreateMap<Enrollee, StatementDto>();
        }
    }
}