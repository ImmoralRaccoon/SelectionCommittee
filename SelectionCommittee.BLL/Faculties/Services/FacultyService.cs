using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SelectionCommittee.BLL.Enrollees;
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

            _logger.LogInfo("GetAllAsync() method from SelectionCommittee.BLL.Faculties.Services.FacultyService has been finished.");
            return facultyDtos;
        }

        public async Task<IEnumerable<FacultyDto>> GetAllSortedByNameFromAAsync()
        {
            var faculties = await _selectionCommitteeDataStorage.FacultyRepository.GetAll().ToListAsync();
            var orderedEnumerable = faculties.OrderBy(f => f.Name);
            var facultyDtos = _mapper.Map<IEnumerable<FacultyDto>>(orderedEnumerable);

            _logger.LogInfo("GetAllSortedByNameFromAAsync() from SelectionCommittee.BLL.Faculties.Services.FacultyService has been finished.");
            return facultyDtos;
        }

        public async Task<IEnumerable<FacultyDto>> GetAllSortedByNameFromZAsync()
        {
            var faculties = await _selectionCommitteeDataStorage.FacultyRepository.GetAll().ToListAsync();
            var orderedEnumerable = faculties.OrderByDescending(f => f.Name);
            var facultyDtos = _mapper.Map<IEnumerable<FacultyDto>>(orderedEnumerable);

            _logger.LogInfo("GetAllSortedByNameFromZAsync() from SelectionCommittee.BLL.Faculties.Services.FacultyService has been finished.");
            return facultyDtos;
        }

        public async Task<IEnumerable<FacultyDto>> GetAllSortedByAmountOfPlacesAsync()
        {
            var faculties = await _selectionCommitteeDataStorage.FacultyRepository.GetAll().ToListAsync();
            var orderedEnumerable = faculties.OrderByDescending(f => f.NumberOfPlaces);
            var facultyDtos = _mapper.Map<IEnumerable<FacultyDto>>(orderedEnumerable);

            _logger.LogInfo("GetAllSortedByAmountOfPlacesAsync() from SelectionCommittee.BLL.Faculties.Services.FacultyService has been finished.");
            return facultyDtos;
        }

        public async Task<IEnumerable<FacultyDto>> GetAllSortedByAmountofBudgetPlacesAsync()
        {
            var faculties = await _selectionCommitteeDataStorage.FacultyRepository.GetAll().ToListAsync();
            var orderedEnumerable = faculties.OrderByDescending(f => f.NumberOfBudgetPlaces);
            var facultyDtos = _mapper.Map<IEnumerable<FacultyDto>>(orderedEnumerable);

            _logger.LogInfo("GetAllSortedByAmountofBudgetPlacesAsync() from SelectionCommittee.BLL.Faculties.Services.FacultyService has been finished.");
            return facultyDtos;
        }

        public async Task<FacultyDto> GetAsync(int id)
        {
            var faculty = await _selectionCommitteeDataStorage.FacultyRepository.GetAsync(id);
            var facultyDto = _mapper.Map<FacultyDto>(faculty);

            _logger.LogInfo("GetAsync(int id) from SelectionCommittee.BLL.Faculties.Services.FacultyService has been finished.");
            return facultyDto;
        }

        public async Task<int> AddAsync(FacultyCreateDto facultyCreateDto)
        {
            //if (string.IsNullOrEmpty(facultyCreateDto.Name))
            //    return -1;
            //if (facultyCreateDto.NumberOfPlaces == 0)
            //    return -2;
            //if (facultyCreateDto.NumberOfBudgetPlaces == 0)
            //    return -3;

            var faculty = _mapper.Map<Faculty>(facultyCreateDto);
            await _selectionCommitteeDataStorage.FacultyRepository.AddAsync(faculty);
            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("AddAsync(FacultyCreateDto facultyCreateDto) from SelectionCommittee.BLL.Faculties.Services.FacultyService has been finished.");
            return faculty.Id;
        }

        public async Task<int> UpdateAsync(FacultyUpdateDto facultyUpdateDto)
        {
            if (string.IsNullOrEmpty(facultyUpdateDto.Name))
                return -1;
            if (facultyUpdateDto.NumberOfPlaces == 0)
                return -2;
            if (facultyUpdateDto.NumberOfBudgetPlaces == 0)
                return -3;

            var faculty = await _selectionCommitteeDataStorage.FacultyRepository.GetAsync(facultyUpdateDto.Id);

            faculty.Name = facultyUpdateDto.Name;
            faculty.NumberOfBudgetPlaces = facultyUpdateDto.NumberOfBudgetPlaces;
            faculty.NumberOfPlaces = facultyUpdateDto.NumberOfPlaces;

            _selectionCommitteeDataStorage.FacultyRepository.Update(faculty);
            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("UpdateAsync(FacultyUpdateDto facultyUpdateDto) from SelectionCommittee.BLL.Faculties.Services.FacultyService has been finished.");
            return faculty.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            if (!await _selectionCommitteeDataStorage.FacultyRepository.ContainsEntityWithId(id))
                return -4;

            _selectionCommitteeDataStorage.FacultyRepository.Delete(id);

            var facultyId = await _selectionCommitteeDataStorage.FacultyEnrolleeRepository.GetByFacultyId(id);
            await _selectionCommitteeDataStorage.FacultyEnrolleeRepository.RemoveRange(facultyId);
            await _selectionCommitteeDataStorage.SaveChangesAsync();

            _logger.LogInfo("DeleteAsync(int id) from SelectionCommittee.BLL.Faculties.Services.FacultyService has been finished.");
            return 1;
        }

        public async Task<IEnumerable<StatementDto>> GetFacultyEnrollees(int id)
        {
            var faculty = await _selectionCommitteeDataStorage.FacultyRepository.GetAsync(id);
            var facultyEnrollees = faculty.FacultyEnrolles.Where(fe => fe.FacultyId == id).Select(e => e.Enrollee)
                .OrderByDescending(s => s.Rating).Take(faculty.NumberOfBudgetPlaces);
            var result = _mapper.Map<IEnumerable<StatementDto>>(facultyEnrollees);

            _logger.LogInfo("GetFacultyEnrolleeIds(int id) from SelectionCommittee.BLL.Faculties.Services.FacultyService has been finished.");
            return result;
        }
    }
}