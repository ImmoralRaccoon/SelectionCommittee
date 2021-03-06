﻿using AutoMapper;
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
            CreateMap<FacultyUpdateDto, Faculty>();
            CreateMap<Enrollee, StatementDto>();
        }
    }
}