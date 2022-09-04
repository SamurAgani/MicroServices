using FreeCource.Shared.ControllerBases;
using FreeCource.Shared.Services;
using FreeCourse.Services.Order.Application.Command;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        public IMediator  mediator { get; set; }
        public ISharedIdentityService  sharedIdentityService { get; set; }
        public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
        {
            this.mediator = mediator;
            this.sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = await mediator.Send(new GetOrdersByUserIdQuery() { UserId = sharedIdentityService.GetUserId});
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand)
        {
            createOrderCommand.BuyerId = sharedIdentityService.GetUserId;
            var response = await mediator.Send(createOrderCommand);
            return CreateActionResultInstance(response);
        }
    }
}
