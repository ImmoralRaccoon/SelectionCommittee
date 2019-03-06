using AutoMapper;
using SelectionCommittee.API.Models.Assessments;
using SelectionCommittee.BLL.Assessments;

namespace SelectionCommittee.API.MappingConfiguration
{
    public class AssessmentDtoModelMappingProfile : Profile
    {
        public AssessmentDtoModelMappingProfile()
        {
            CreateMap<AssessmentDto, AssessmentModel>()
                .ForPath(dst => dst.EnrolleeInfoModel.Id, src => src.MapFrom(ad => ad.EnrolleeId))
                .ForPath(dst => dst.EnrolleeInfoModel.FirstName, src => src.MapFrom(ad => ad.FirstName))
                .ForPath(dst => dst.EnrolleeInfoModel.LastName, src => src.MapFrom(ad => ad.LastName))
                .ForPath(dst => dst.EnrolleeInfoModel.Patronymic, src => src.MapFrom(ad => ad.Patronymic))
                .ForPath(dst => dst.EnrolleeInfoModel.Email, src => src.MapFrom(ad => ad.Email))
                .ForPath(dst => dst.EnrolleeInfoModel.City, src => src.MapFrom(ad => ad.City))
                .ForPath(dst => dst.EnrolleeInfoModel.Region, src => src.MapFrom(ad => ad.Region));
            CreateMap<AssessmentAddOrUpdateModel, AssessmentCreateDto>();
            CreateMap<AssessmentAddOrUpdateModel, AssessmentUpdateDto>();
        }
    }
}