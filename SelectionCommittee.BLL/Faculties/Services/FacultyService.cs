using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SelectionCommittee.DAL.Entities;
using SelectionCommittee.DAL.UnitOfWork;
using SelectionCommittee.Logger;

namespace SelectionCommittee.BLL.Faculties.Services
{
    public class FacultyService : IFacultyService
    {
        private readonly IUnitOfWork _selectionCommitteeDataStorage;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public FacultyService(IUnitOfWork selectionCommitteeDataStorage, IMapper mapper, ILoggerManager logger)
        {
            _selectionCommitteeDataStorage = selectionCommitteeDataStorage;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<FacultyDto>> GetAllAsync()
        {
            var faculties = await _selectionCommitteeDataStorage.FacultyRepository.GetAll().ToListAsync();
            var facultyDtos = _mapper.Map<IEnumerable<FacultyDto>>(faculties);
            return facultyDtos;
        }

        public async Task<IEnumerable<FacultyDto>> GetAllSortedByNameFromAAsync()
        {
            var faculties = await _selectionCommitteeDataStorage.FacultyRepository.GetAll().ToListAsync();
            var orderedEnumerable = faculties.OrderBy(f => f.Name);
            var facultyDtos = _mapper.Map<IEnumerable<FacultyDto>>(orderedEnumerable);
            _logger.LogInfo("Service");
            return facultyDtos;
        }

        public async Task<IEnumerable<FacultyDto>> GetAllSortedByNameFromZAsync()
        {
            var faculties = await _selectionCommitteeDataStorage.FacultyRepository.GetAll().ToListAsync();
            var orderedEnumerable = faculties.OrderByDescending(f => f.Name);
            var facultyDtos = _mapper.Map<IEnumerable<FacultyDto>>(orderedEnumerable);
            return facultyDtos;
        }

        public async Task<IEnumerable<FacultyDto>> GetAllSortedByAmountOfPlacesAsync()
        {
            var faculties = await _selectionCommitteeDataStorage.FacultyRepository.GetAll().ToListAsync();
            var orderedEnumerable = faculties.OrderByDescending(f => f.NumberOfPlaces);
            var facultyDtos = _mapper.Map<IEnumerable<FacultyDto>>(orderedEnumerable);
            return facultyDtos;
        }

        public async Task<IEnumerable<FacultyDto>> GetAllSortedByAmountofBudgetPlacesAsync()
        {
            var faculties = await _selectionCommitteeDataStorage.FacultyRepository.GetAll().ToListAsync();
            var orderedEnumerable = faculties.OrderByDescending(f => f.NumberOfBudgetPlaces);
            var facultyDtos = _mapper.Map<IEnumerable<FacultyDto>>(orderedEnumerable);
            return facultyDtos;
        }

        public async Task<FacultyDto> GetAsync(int id)
        {
            var faculty = await _selectionCommitteeDataStorage.FacultyRepository.GetAsync(id);
            var facultyDto = _mapper.Map<FacultyDto>(faculty);
            return facultyDto;
        }

        public async Task<int> AddAsync(FacultyCreateDto facultyCreateDto)
        {
            var faculty = _mapper.Map<Faculty>(facultyCreateDto);
            await _selectionCommitteeDataStorage.FacultyRepository.AddAsync(faculty);
            await _selectionCommitteeDataStorage.SaveChangesAsync();
            return faculty.Id;
        }

        public async Task<int> UpdateAsync(FacultyUpdateDto facultyUpdateDto)
        {
            var faculty = await _selectionCommitteeDataStorage.FacultyRepository.GetAsync(facultyUpdateDto.Id);

            faculty.Name = facultyUpdateDto.Name;
            faculty.NumberOfBudgetPlaces = facultyUpdateDto.NumberOfBudgetPlaces;
            faculty.NumberOfPlaces = facultyUpdateDto.NumberOfPlaces;

            _selectionCommitteeDataStorage.FacultyRepository.Update(faculty);
            await _selectionCommitteeDataStorage.SaveChangesAsync();
            return faculty.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            _selectionCommitteeDataStorage.FacultyRepository.Delete(id);

            var facultyId = await _selectionCommitteeDataStorage.FacultyEnrolleeRepository.GetByFacultyId(id);
            await _selectionCommitteeDataStorage.FacultyEnrolleeRepository.RemoveRange(facultyId);

            await _selectionCommitteeDataStorage.SaveChangesAsync();
            return 1;
        }

        public async Task<IEnumerable<int>> GetFacultyEnrolleeIds(int id)
        {
            var faculty = await _selectionCommitteeDataStorage.FacultyRepository.GetAsync(id);
            var facultyEnrollees = faculty.FacultyEnrolles.Where(fe => fe.FacultyId == id).Select(e => e.EnrolleeId);
            //var facultyEnrolleesSorted;
            return facultyEnrollees;
        }
    }
}