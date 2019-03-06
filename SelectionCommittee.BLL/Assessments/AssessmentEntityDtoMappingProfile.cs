using AutoMapper;
using SelectionCommittee.DAL.Entities;

namespace SelectionCommittee.BLL.Assessments
{
    public class AssessmentEntityDtoMappingProfile : Profile
    {
        public AssessmentEntityDtoMappingProfile()
        {
            CreateMap<Assessment, AssessmentDto>()
                .ForMember(dst => dst.EnrolleeId, src => src.MapFrom(a => a.Enrollee.Id))
                .ForMember(dst => dst.FirstName, src => src.MapFrom(a => a.Enrollee.FirstName))
                .ForMember(dst => dst.LastName, src => src.MapFrom(a => a.Enrollee.LastName))
                .ForMember(dst => dst.Patronymic, src => src.MapFrom(a => a.Enrollee.Patronymic))
                .ForMember(dst => dst.Email, src => src.MapFrom(a => a.Enrollee.Email))
                .ForMember(dst => dst.City, src => src.MapFrom(a => a.Enrollee.City))
                .ForMember(dst => dst.Region, src => src.MapFrom(a => a.Enrollee.Region))
                .ForMember(dst => dst.SchoolLyceumName, src => src.MapFrom(a => a.Enrollee.SchoolLyceumName));
            CreateMap<AssessmentDto, Assessment>()
                .ForPath(dst => dst.Enrollee.Id, src => src.MapFrom(ad => ad.EnrolleeId))
                .ForPath(dst => dst.Enrollee.FirstName, src => src.MapFrom(ad => ad.FirstName))
                .ForPath(dst => dst.Enrollee.LastName, src => src.MapFrom(ad => ad.LastName))
                .ForPath(dst => dst.Enrollee.Patronymic, src => src.MapFrom(ad => ad.Patronymic))
                .ForPath(dst => dst.Enrollee.Email, src => src.MapFrom(ad => ad.Email))
                .ForPath(dst => dst.Enrollee.City, src => src.MapFrom(ad => ad.City))
                .ForPath(dst => dst.Enrollee.Region, src => src.MapFrom(ad => ad.Region))
                .ForPath(dst => dst.Enrollee.SchoolLyceumName, src => src.MapFrom(ad => ad.SchoolLyceumName));
        }
    }
}