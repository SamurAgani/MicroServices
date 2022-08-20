using FreeCource.Shared.Services;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICatalogService catalogService;

        private readonly ISharedIdentityService sharedIdentityService;

        public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            this.catalogService = catalogService;
            this.sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await catalogService.GetAllCourseByUserId(sharedIdentityService.GetUserId));
        }
        public async Task<IActionResult> Create()
        {
            var catagories = await catalogService.GetAllCategory();
            ViewBag.categoryList = new SelectList(catagories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInput courseCreateInput)
        {
            var catagories = await catalogService.GetAllCategory();
            ViewBag.categoryList = new SelectList(catagories, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return View();
            }
            courseCreateInput.UserId = sharedIdentityService.GetUserId;
            await catalogService.CreateCourseAsync(courseCreateInput);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(string id)
        {
            var course = await catalogService.GetByCourseId(id);

            var catagories = await catalogService.GetAllCategory();
            if (course == null)
            {
                RedirectToAction("Index");
            }
            CourseUpdateInput courseUpdateInput = new CourseUpdateInput
            {
                Id = course.Id,
                Name = course.Name,
                Price = course.Price,
                Feature = course.Feature,
                CategoryId = course.CategoryId,
                UserId = course.UserId,
                Description = course.Description,
                Picture = course.Picture
            };

            ViewBag.categoryList = new SelectList(catagories, "Id", "Name");
            return View(courseUpdateInput);

        }
        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateInput courseUpdateInput)
        {
            var catagories = await catalogService.GetAllCategory();
            ViewBag.categoryList = new SelectList(catagories, "Id", "Name", courseUpdateInput.Id);
            if (!ModelState.IsValid)
            {
                return View();
            }
            await catalogService.UpdateCourseAsync(courseUpdateInput);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await catalogService.DeleteCourseAsync(id);
            return RedirectToAction("Index");
        }
    }
}
