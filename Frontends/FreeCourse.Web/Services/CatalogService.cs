using FreeCource.Shared.Dtos;
using FreeCourse.Web.Helpers;
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
        private readonly IPhotoStockService photoStockService;
        private readonly PhotoHelper photoHelper;
        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            this.photoStockService = photoStockService;
            this.httpClient = httpClient;
            this.photoHelper = photoHelper;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            var resultPhoto = await photoStockService.UploadPhoto(courseCreateInput.PhotoFormFile);
            if(resultPhoto != null)
            {
                courseCreateInput.Picture = resultPhoto.Url;
            }

            var response = await httpClient.PostAsJsonAsync<CourseCreateInput>("courses/Create", courseCreateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await httpClient.DeleteAsync($"courses/{courseId}");
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
            var response = await httpClient.GetAsync("courses");
            if (!response.IsSuccessStatusCode)
                return null;
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseVM>>>();
            responseSuccess.Data.ForEach(x =>
            {
                x.StockPictureUrl = photoHelper.GetPhotoStockUrl(x.Picture);
            });
            return responseSuccess.Data;
        }

        public async Task<List<CourseVM>> GetAllCourseByUserId(string userId)
        {
            var response = await httpClient.GetAsync($"courses/GetByUserId/{userId}");
            if (!response.IsSuccessStatusCode)
                return null;
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseVM>>>();

            responseSuccess.Data.ForEach(x =>
            {
                x.StockPictureUrl = photoHelper.GetPhotoStockUrl(x.Picture);
            });
            return responseSuccess.Data;
        }

        public async Task<CourseVM> GetByCourseId(string courseId)
        {
            var response = await httpClient.GetAsync($"courses/GetById/{courseId}");
            if (!response.IsSuccessStatusCode)
                return null;
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CourseVM>>();
            responseSuccess.Data.StockPictureUrl = photoHelper.GetPhotoStockUrl(responseSuccess.Data.Picture);
            return responseSuccess.Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            var resultPhoto = await photoStockService.UploadPhoto(courseUpdateInput.PhotoFormFile);
            if (resultPhoto != null)
            {
                await photoStockService.DeletePhoto(courseUpdateInput.Picture);
                courseUpdateInput.Picture = resultPhoto.Url;
            }
            var response = await httpClient.PutAsJsonAsync<CourseUpdateInput>("courses", courseUpdateInput);
            return response.IsSuccessStatusCode;
        }
    }
}
