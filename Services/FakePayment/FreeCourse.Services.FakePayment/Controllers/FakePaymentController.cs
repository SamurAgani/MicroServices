using FreeCource.Shared.ControllerBases;
using FreeCource.Shared.Dtos;
using FreeCource.Shared.Messages;
using FreeCourse.Services.FakePayment.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentController : CustomBaseController
    {

        public readonly ISendEndpointProvider sendEndpointProvider;

        public FakePaymentController(ISendEndpointProvider sendEndpointProvider)
        {
            this.sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
            var sendEndPoint = await sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:create-order-service"));
            var createOrderMessageCommand = new CreateOrderMessageCommand();
            createOrderMessageCommand.BuyerId = paymentDto.Order.BuyerId;
            createOrderMessageCommand.Province = paymentDto.Order.Address.Province;
            createOrderMessageCommand.Street = paymentDto.Order.Address.Street;
            createOrderMessageCommand.Line = paymentDto.Order.Address.Line;
            createOrderMessageCommand.District = paymentDto.Order.Address.District;

            paymentDto.Order.OrderItems.ForEach(i =>
            {
                createOrderMessageCommand.OrderItems.Add(new FreeCource.Shared.Messages.OrderItemDto()
                {
                    ProductUrl = i.ProductUrl,
                    Price = i.Price,
                    ProductId = i.ProductId,
                    ProductName = i.ProductName
                });

            });
            await sendEndPoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);

            return CreateActionResultInstance(FreeCource.Shared.Dtos.Response<NoContent>.Success(200));
        }
    }
}
