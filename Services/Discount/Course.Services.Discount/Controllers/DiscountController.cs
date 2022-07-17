using Course.Services.Discount.Services;
using FreeCource.Shared.ControllerBases;
using FreeCource.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Course.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : CustomBaseController
    {
        private readonly IDiscountService discountService;
        private readonly ISharedIdentityService sharedIdentityService;

        public DiscountController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            this.discountService = discountService;
            this.sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await discountService.GetAll());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            return CreateActionResultInstance(await discountService.GetById(Id));
        }

        [HttpGet]
        [Route("/api/[controller]/[action]/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var userId = sharedIdentityService.GetUserId();
            return CreateActionResultInstance(await discountService.GetByCodeAndUserId(code,userId));
        }

        [HttpPost]
        public async Task<IActionResult> Save(Model.Discount discount)
        {
            return CreateActionResultInstance(await discountService.Save(discount));

        }

        [HttpPut]
        public async Task<IActionResult> Update(Model.Discount discount)
        {
            return CreateActionResultInstance(await discountService.Update(discount));

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            return CreateActionResultInstance(await discountService.Delete(Id));
        }
    }
}
