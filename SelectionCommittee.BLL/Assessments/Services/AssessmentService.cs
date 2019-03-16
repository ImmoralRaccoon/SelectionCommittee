﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SelectionCommittee.DAL.Entities;
using SelectionCommittee.DAL.UnitOfWork;
using SelectionCommittee.Logger;

namespace SelectionCommittee.BLL.Assessments.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IUnitOfWork _selectionCommitteeDataStorage;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public AssessmentService(IUnitOfWork selectionCommitteeDataStorage, IMapper mapper, ILoggerManager logger)
        {
            _selectionCommitteeDataStorage = selectionCommitteeDataStorage;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<AssessmentDto>> GetAllAsync()
        {
            var assessments = await _selectionCommitteeDataStorage.AssessmentRepository.GetAll().ToListAsync();
            var assessmentsDtos = _mapper.Map<IEnumerable<AssessmentDto>>(assessments);

            _logger.LogInfo("GetAllAsync() method from SelectionCommittee.BLL.Assessments.Services.AssessmentService has been finished.");
            return assessmentsDtos;
        }

        public async Task<AssessmentDto> GetAsync(int id)
        {
            var assessmnet = await _selectionCommitteeDataStorage.AssessmentRepository.GetAsync(id);
            var assessmentDto = _mapper.Map<AssessmentDto>(assessmnet);

            _logger.LogInfo("GetAsync(int id) method from SelectionCommittee.BLL.Assessments.Services.AssessmentService has been finished.");
            return assessmentDto;
        }

        public async Task<int> AddAsync(AssessmentCreateDto assessmentCreateDto)
        {
            if (assessmentCreateDto.EnrolleeId == 0)
                return -1;
            if (string.IsNullOrEmpty(assessmentCreateDto.Name))
                return -2;
            if (string.IsNullOrEmpty(assessmentCreateDto.GradeType))
                return -3;
            if (assessmentCreateDto.Grade == 0)
                return -4;

            var enrollee = await _selectionCommitteeDataStorage.EnrolleeRepository.GetAsync(assessmentCreateDto.EnrolleeId);

            var assessment = new Assessment
            {
                EnrolleeId = enrollee.Id,
                Enrollee = enrollee,

                Name = assessmentCreateDto.Name,
                Grade = assessmentCreateDto.Grade,
                GradeType = assessmentCreateDto.GradeType
            };

            await _selectionCommitteeDataStorage.AssessmentRepository.AddAsync(assessment);
            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("AddAsync(AssessmentCreateDto assessmentCreateDto) method from SelectionCommittee.BLL.Assessments.Services.AssessmentService has been finished.");
            return assessment.EnrolleeId;
        }

        public async Task<int> UpdateAsync(AssessmentUpdateDto assessmentUpdateDto)
        {
            if (assessmentUpdateDto.EnrolleId == 0)
                return -1;
            if (string.IsNullOrEmpty(assessmentUpdateDto.Name))
                return -2;
            if (string.IsNullOrEmpty(assessmentUpdateDto.GradeType))
                return -3;
            if (assessmentUpdateDto.Grade == 0)
                return -4;

            var assessment = await _selectionCommitteeDataStorage.AssessmentRepository.GetAsync(assessmentUpdateDto.Id);

            assessment.Name = assessmentUpdateDto.Name;
            assessment.Grade = assessmentUpdateDto.Grade;
            assessment.GradeType = assessmentUpdateDto.GradeType;

            _selectionCommitteeDataStorage.AssessmentRepository.Update(assessment);
            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("UpdateAsync(AssessmentUpdateDto assessmentUpdateDto) method from SelectionCommittee.BLL.Assessments.Services.AssessmentService has been finished.");
            return assessment.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            if (!await _selectionCommitteeDataStorage.AssessmentRepository.ContainsEntityWithId(id))
                return -5;

            _selectionCommitteeDataStorage.AssessmentRepository.Delete(id);
            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("DeleteAsync(int id) method from SelectionCommittee.BLL.Assessments.Services.AssessmentService has been finished.");
            return 1;
        }
    }
}