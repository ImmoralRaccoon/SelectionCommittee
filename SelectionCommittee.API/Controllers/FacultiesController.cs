﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelectionCommittee.API.Models.Faculties;
using SelectionCommittee.BLL.Faculties;
using SelectionCommittee.BLL.Faculties.Services;

namespace SelectionCommittee.API.Controllers
{
    [Route("api/[controller]")]
    public class FacultiesController : Controller
    {
        private readonly IFacultyService _facultyService;
        private readonly IMapper _mapper;

        public FacultiesController(IFacultyService facultyService, IMapper mapper)
        {
            _facultyService = facultyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var facultyDtos = await _facultyService.GetAllAsync();
            var facultyModels = _mapper.Map<IEnumerable<FacultyModel>>(facultyDtos);
            return Ok(facultyModels);
        }

        [HttpGet("{id}", Name = "GetFaculty")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var facultyDto = await _facultyService.GetAsync(id);
            var facultyModel = _mapper.Map<FacultyModel>(facultyDto);
            return Ok(facultyModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] FacultyAddOrUpdateModel facultyAddOrUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            var facultyDto = _mapper.Map<FacultyCreateDto>(facultyAddOrUpdateModel);
            var facultyCreateModel = await _facultyService.AddAsync(facultyDto);
            return Ok(facultyCreateModel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int? id,
            [FromBody] FacultyAddOrUpdateModel facultyAddOrUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }
            if (id.HasValue)
            {
                var facultyUpdateDto = _mapper.Map<FacultyUpdateDto>(facultyAddOrUpdateModel);
                facultyUpdateDto.Id = id.Value;
                var faculty = await _facultyService.UpdateAsync(facultyUpdateDto);
                return Ok(faculty);
            }
            else
            {
                var facultyDto = _mapper.Map<FacultyCreateDto>(facultyAddOrUpdateModel);
                var facultyCreateModel = await _facultyService.AddAsync(facultyDto);
                return Ok(facultyCreateModel);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _facultyService.DeleteAsync(id);
            return Ok(response);
        }
    }
}