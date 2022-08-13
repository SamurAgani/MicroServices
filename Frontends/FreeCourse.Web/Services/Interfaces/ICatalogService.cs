using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CourseVM>> GetAllCourse();
        Task<List<CategoryVM>> GetAllCategory();
        Task<List<CourseVM>> GetAllCourseByUserId(string userId);
        Task<bool> DeleteCourseAsync(string courseId);

        Task<CourseVM> GetByCourseId(string courseId);

        Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput);
        Task<bool> UpdateCourseAsync(CourseUpdateInput courseCreateInput);

    }
}
