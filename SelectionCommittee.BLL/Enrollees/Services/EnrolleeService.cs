using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SelectionCommittee.BLL.Assessments;
using SelectionCommittee.DAL.Entities;
using SelectionCommittee.DAL.UnitOfWork;

namespace SelectionCommittee.BLL.Enrollees.Services
{
    public class EnrolleeService : IEnrolleeService
    {
        private readonly IUnitOfWork _selectionCommitteeDataStorage;
        private readonly IMapper _mapper;

        public EnrolleeService(IUnitOfWork selectionCommitteeDataStorage, IMapper mapper)
        {
            _selectionCommitteeDataStorage = selectionCommitteeDataStorage;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EnrolleDto>> GetAllAsync()
        {
            var enrollees = await _selectionCommitteeDataStorage.EnrolleeRepository.GetAll().ToListAsync();
            var enrolleeDtos = _mapper.Map<IEnumerable<EnrolleDto>>(enrollees);
            return enrolleeDtos;
        }

        public async Task<EnrolleDto> GetAsync(int id)
        {
            var enrollee = await _selectionCommitteeDataStorage.EnrolleeRepository.GetAsync(id);
            var enrolleeDto = _mapper.Map<EnrolleDto>(enrollee);
            return enrolleeDto;
        }

        public async Task<int> AddAsync(EnrolleCreateDto enrolleCreateDto)
        {
            var enrollee = _mapper.Map<Enrollee>(enrolleCreateDto);
            await _selectionCommitteeDataStorage.EnrolleeRepository.AddAsync(enrollee);
            await _selectionCommitteeDataStorage.SaveChangesAsync();
            return enrollee.Id;
        }

        public async Task<int> AddFacultyEnrolleeAsync(FacultyEnrolleeCreateDto facultyEnrolleeCreateDto)
        {
            var facultyEnrollee = _mapper.Map<FacultyEnrollee>(facultyEnrolleeCreateDto);
            await _selectionCommitteeDataStorage.FacultyEnrolleeRepository.AddAsync(facultyEnrollee);
            await _selectionCommitteeDataStorage.SaveChangesAsync();
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
            return enrollee.Id;
        }

        public async Task<int> UpdateStatusAsync(EnrolleeUpdateStatusDto enrolleeUpdateStatusDto)
        {
            var enrollee = await _selectionCommitteeDataStorage.EnrolleeRepository.GetAsync(enrolleeUpdateStatusDto.Id);
            enrollee.LockStatus = enrolleeUpdateStatusDto.LockStatus;

            _selectionCommitteeDataStorage.EnrolleeRepository.Update(enrollee);
            await _selectionCommitteeDataStorage.SaveChangesAsync();
            return enrollee.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            _selectionCommitteeDataStorage.EnrolleeRepository.Delete(id);
            var enrolleeId = await _selectionCommitteeDataStorage.FacultyEnrolleeRepository.GetByEnrolleeId(id);
            await _selectionCommitteeDataStorage.FacultyEnrolleeRepository.RemoveRange(enrolleeId);

            await _selectionCommitteeDataStorage.SaveChangesAsync();
            return 1;
        }
    }
}