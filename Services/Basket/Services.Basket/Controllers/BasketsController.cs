using FreeCource.Shared.ControllerBases;
using FreeCource.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Basket.DTOS;
using Services.Basket.Services;
using System.Threading.Tasks;

namespace Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        public IBasketServices basketServices { get; set; }
        public ISharedIdentityService sharedIdentityService { get; set; }


        public BasketsController(IBasketServices basketServices, ISharedIdentityService sharedIdentityService)
        {
            this.basketServices = basketServices;
            this.sharedIdentityService = sharedIdentityService;
        }
        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            return CreateActionResultInstance(await basketServices.GetBasket(sharedIdentityService.GetUserId()));
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
        {
            var response = await basketServices.SaveOrUpdate(basketDto);
            return CreateActionResultInstance(response);

        }
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return CreateActionResultInstance(await basketServices.Delete(sharedIdentityService.GetUserId()));

        }
    }
}
