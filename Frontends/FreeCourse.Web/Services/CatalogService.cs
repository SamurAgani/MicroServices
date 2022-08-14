using FreeCource.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient httpClient;

        public CatalogService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            var response = await httpClient.PostAsJsonAsync<CourseCreateInput>("courses/Create", courseCreateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await httpClient.DeleteAsync($"courses/Delete/{courseId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryVM>> GetAllCategory()
        {
            var response = await httpClient.GetAsync("category/GetAll");
            if (!response.IsSuccessStatusCode)
                return null;
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryVM>>>();
            return responseSuccess.Data;
        }

        public async Task<List<CourseVM>> GetAllCourse()
        {
            var response = await httpClient.GetAsync("courses/GetAll");
            if (!response.IsSuccessStatusCode)
                return null;
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseVM>>>();
            return responseSuccess.Data;
        }

        public async Task<List<CourseVM>> GetAllCourseByUserId(string userId)
        {
            var response = await httpClient.GetAsync($"courses/GetByUserId/{userId}");
            if (!response.IsSuccessStatusCode)
                return null;
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseVM>>>();
            return responseSuccess.Data;
        }

        public async Task<CourseVM> GetByCourseId(string courseId)
        {
            var response = await httpClient.GetAsync($"courses/GetById/{courseId}");
            if (!response.IsSuccessStatusCode)
                return null;
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CourseVM>>();
            return responseSuccess.Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseCreateInput)
        {
            var response = await httpClient.PutAsJsonAsync<CourseUpdateInput>("courses/create", courseCreateInput);
            return response.IsSuccessStatusCode;
        }
    }
}
