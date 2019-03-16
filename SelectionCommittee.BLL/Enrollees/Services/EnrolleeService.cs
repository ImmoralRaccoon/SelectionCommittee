using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            if (string.IsNullOrEmpty(enrolleCreateDto.FirstName))
                return -1;
            if (string.IsNullOrEmpty(enrolleCreateDto.LastName))
                return -2;
            if (string.IsNullOrEmpty(enrolleCreateDto.Patronymic))
                return -3;
            if (string.IsNullOrEmpty(enrolleCreateDto.Email))
                return -4;
            if (string.IsNullOrEmpty(enrolleCreateDto.City))
                return -5;
            if (string.IsNullOrEmpty(enrolleCreateDto.Region))
                return -6;
            if (string.IsNullOrEmpty(enrolleCreateDto.SchoolLyceumName))
                return -7;

            var enrollee = _mapper.Map<Enrollee>(enrolleCreateDto);
            await _selectionCommitteeDataStorage.EnrolleeRepository.AddAsync(enrollee);
            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("AddAsync(EnrolleCreateDto enrolleCreateDto) method from SelectionCommittee.BLL.Enrollees.Services.EnrolleeService has been finished.");
            return enrollee.Id;
        }

        public async Task<int> AddFacultyEnrolleeAsync(FacultyEnrolleeCreateDto facultyEnrolleeCreateDto)
        {
            if (facultyEnrolleeCreateDto.EnrolleeId == 0)
                return -8;
            if (facultyEnrolleeCreateDto.FacultyId == 0)
                return -9;

            var facultyEnrollee = _mapper.Map<FacultyEnrollee>(facultyEnrolleeCreateDto);
            await _selectionCommitteeDataStorage.FacultyEnrolleeRepository.AddAsync(facultyEnrollee);
            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("AddFacultyEnrolleeAsync(FacultyEnrolleeCreateDto facultyEnrolleeCreateDto) method from SelectionCommittee.BLL.Enrollees.Services.EnrolleeService has been finished.");
            return 1;
        }

        public async Task<int> UpdateAsync(EnrolleeUpdateDto enrolleeUpdateDto)
        {
            if (!await _selectionCommitteeDataStorage.EnrolleeRepository.ContainsEntityWithId(enrolleeUpdateDto.Id))
                return -12;
            if (string.IsNullOrEmpty(enrolleeUpdateDto.Email))
                return -4;
            if (string.IsNullOrEmpty(enrolleeUpdateDto.City))
                return -5;
            if (string.IsNullOrEmpty(enrolleeUpdateDto.Region))
                return -6;
            if (string.IsNullOrEmpty(enrolleeUpdateDto.SchoolLyceumName))
                return -7;

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
            if (!await _selectionCommitteeDataStorage.EnrolleeRepository.ContainsEntityWithId(
                enrolleeUpdateStatusDto.Id))
                return -13;
            if (string.IsNullOrEmpty(enrolleeUpdateStatusDto.LockStatus))
                return -10;

            var enrollee = await _selectionCommitteeDataStorage.EnrolleeRepository.GetAsync(enrolleeUpdateStatusDto.Id);
            enrollee.LockStatus = enrolleeUpdateStatusDto.LockStatus;

            _selectionCommitteeDataStorage.EnrolleeRepository.Update(enrollee);
            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("UpdateStatusAsync(EnrolleeUpdateStatusDto enrolleeUpdateStatusDto) method from SelectionCommittee.BLL.Enrollees.Services.EnrolleeService has been finished.");
            return enrollee.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            if (!await _selectionCommitteeDataStorage.EnrolleeRepository.ContainsEntityWithId(id))
                return -11;

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
            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("CalculateRatings() method from SelectionCommittee.BLL.Enrollees.Services.EnrolleeService has been finished.");
            return enrolleeSorted;
        }

        public async Task<string> GetEnrolleEmail(int id)
        {
            var enrollee = await _selectionCommitteeDataStorage.EnrolleeRepository.GetAsync(id);
            var enrolleeDto = _mapper.Map<EnrolleDto>(enrollee);

            _logger.LogInfo("GetEnrolleEmail(int id) method from SelectionCommittee.BLL.Enrollees.Services.EnrolleeService has been finished.");
            return enrolleeDto.Email;
        }
    }
}