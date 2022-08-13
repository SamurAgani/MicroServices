using FreeCource.Shared.Services;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}
