using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SelectionCommittee.BLL.Assessments;
using SelectionCommittee.DAL.Entities;
using SelectionCommittee.DAL.UnitOfWork;
using SelectionCommittee.Logger;

namespace SelectionCommittee.BLL.Enrollees.Services
{
    public class EnrolleeService : IEnrolleeService
    {
        private readonly IUnitOfWork _selectionCommitteeDataStorage;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public EnrolleeService(IUnitOfWork selectionCommitteeDataStorage, IMapper mapper, ILoggerManager logger)
        {
            _selectionCommitteeDataStorage = selectionCommitteeDataStorage;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<EnrolleDto>> GetAllAsync()
        {
            var enrollees = await _selectionCommitteeDataStorage.EnrolleeRepository.GetAll().ToListAsync();
            var enrolleeDtos = _mapper.Map<IEnumerable<EnrolleDto>>(enrollees);

            _logger.LogInfo("GetAllAsync() method from SelectionCommittee.BLL.Enrollees.Services.EnrolleeService has been finished.");
            return enrolleeDtos;
        }

        public async Task<EnrolleDto> GetAsync(int id)
        {
            var enrollee = await _selectionCommitteeDataStorage.EnrolleeRepository.GetAsync(id);
            var enrolleeDto = _mapper.Map<EnrolleDto>(enrollee);

            _logger.LogInfo("GetAsync(int id) method from SelectionCommittee.BLL.Enrollees.Services.EnrolleeService has been finished.");
            return enrolleeDto;
        }

        public async Task<int> AddAsync(EnrolleCreateDto enrolleCreateDto)
        {
            var enrollee = _mapper.Map<Enrollee>(enrolleCreateDto);
            await _selectionCommitteeDataStorage.EnrolleeRepository.AddAsync(enrollee);
            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("AddAsync(EnrolleCreateDto enrolleCreateDto) method from SelectionCommittee.BLL.Enrollees.Services.EnrolleeService has been finished.");
            return enrollee.Id;
        }

        public async Task<int> AddFacultyEnrolleeAsync(FacultyEnrolleeCreateDto facultyEnrolleeCreateDto)
        {
            var facultyEnrollee = _mapper.Map<FacultyEnrollee>(facultyEnrolleeCreateDto);
            await _selectionCommitteeDataStorage.FacultyEnrolleeRepository.AddAsync(facultyEnrollee);
            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("AddFacultyEnrolleeAsync(FacultyEnrolleeCreateDto facultyEnrolleeCreateDto) method from SelectionCommittee.BLL.Enrollees.Services.EnrolleeService has been finished.");
            return 1;
        }

        public async Task<int> UpdateAsync(EnrolleeUpdateDto enrolleeUpdateDto)
        {
            var enrollee = await _selectionCommitteeDataStorage.EnrolleeRepository.GetAsync(enrolleeUpdateDto.Id);

            enrollee.City = enrolleeUpdateDto.City;
            enrollee.Email = enrolleeUpdateDto.Email;
            enrollee.Region = enrolleeUpdateDto.Region;
            enrollee.SchoolLyceumName = enrolleeUpdateDto.SchoolLyceumName;

            _selectionCommitteeDataStorage.EnrolleeRepository.Update(enrollee);
            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("UpdateAsync(EnrolleeUpdateDto enrolleeUpdateDto) method from SelectionCommittee.BLL.Enrollees.Services.EnrolleeService has been finished.");
            return enrollee.Id;
        }

        public async Task<int> UpdateStatusAsync(EnrolleeUpdateStatusDto enrolleeUpdateStatusDto)
        {
            var enrollee = await _selectionCommitteeDataStorage.EnrolleeRepository.GetAsync(enrolleeUpdateStatusDto.Id);
            enrollee.LockStatus = enrolleeUpdateStatusDto.LockStatus;

            _selectionCommitteeDataStorage.EnrolleeRepository.Update(enrollee);
            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("UpdateStatusAsync(EnrolleeUpdateStatusDto enrolleeUpdateStatusDto) method from SelectionCommittee.BLL.Enrollees.Services.EnrolleeService has been finished.");
            return enrollee.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            _selectionCommitteeDataStorage.EnrolleeRepository.Delete(id);
            var enrolleeId = await _selectionCommitteeDataStorage.FacultyEnrolleeRepository.GetByEnrolleeId(id);
            await _selectionCommitteeDataStorage.FacultyEnrolleeRepository.RemoveRange(enrolleeId);

            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("DeleteAsync(int id) method from SelectionCommittee.BLL.Enrollees.Services.EnrolleeService has been finished.");
            return 1;
        }

        public async Task<IEnumerable<Enrollee>> CalculateRatings()
        {
            var enrollees = await _selectionCommitteeDataStorage.EnrolleeRepository.GetAll().ToListAsync();


            foreach (Enrollee enrollee in enrollees)
            {
                enrollee.Rating = (double)enrollee.Assessments.Sum(a => a.Grade) / enrollee.Assessments.Count;
            }

            var enrolleeSorted = enrollees.OrderBy(e => e.Rating);

            // Sorting by Enrollee.Rating
            //for (int i = 0; i < enrollees.Count - 1; i++)
            //{
            //    for (int j = i + 1; j < enrollees.Count; j++)
            //    {
            //        if (enrollees[i].Rating > enrollees[j].Rating)
            //        {
            //            var tmp = enrollees[i];
            //            enrollees[i] = enrollees[j];
            //            enrollees[j] = tmp;
            //        }
            //    }
            //}

            return enrolleeSorted;
        }

        public async Task<IEnumerable<Enrollee>> GetAllEnrolleeByFacultyId(IEnumerable<int> ints)
        {
            throw new NotImplementedException();
        }

        public async Task<double> CalculateRating(int id)
        {
            var enrollee = await _selectionCommitteeDataStorage.EnrolleeRepository.GetAsync(id);
            var score = enrollee.Assessments.Sum(a => a.Grade);
            double rating = (double)score / 8;
            enrollee.Rating = rating;

            return enrollee.Rating;
        }
    }
}