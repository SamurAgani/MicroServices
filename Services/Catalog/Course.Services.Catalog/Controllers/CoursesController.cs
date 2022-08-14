using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Services;
using FreeCource.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : CustomBaseController
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet]
        [Route("/api/[controller]/GetById/{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            var response = await _courseService.GetById(Id);
            return CreateActionResultInstance(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        [HttpGet]
        [Route("/api/[controller]/GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(string userid)
        {
            var response = await _courseService.GetByUserId(userid);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        [Route("/api/[controller]/Create")]
        public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
        {
            var response = await _courseService.CreateAsync(courseCreateDto);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto courseCreateDto)
        {
            var response = await _courseService.UpdateAsync(courseCreateDto);
            return CreateActionResultInstance(response);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteById(string Id)
        {
            var response = await _courseService.DeleteAsync(Id);
            return CreateActionResultInstance(response);
        }

    }
}
