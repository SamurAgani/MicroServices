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
    }
}
