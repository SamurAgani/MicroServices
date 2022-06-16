using Course.Services.Catalog.Dtos;
using FreeCource.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Services
{
    public interface ICourseService
    {
       public Task<Response<List<CourseDto>>> GetAllAsync();
       public Task<Response<CourseDto>> GetById(string Id);
       public Task<Response<List<CourseDto>>> GetByUserId(string userId);
       public Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
       public Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);
       public Task<Response<NoContent>> DeleteAsync(string Id);
    }
}
