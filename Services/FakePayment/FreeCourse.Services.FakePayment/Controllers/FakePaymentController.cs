using FreeCource.Shared.ControllerBases;
using FreeCource.Shared.Dtos;
using FreeCourse.Services.FakePayment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment(PaymentDto paymentDto)
        {
            return CreateActionResultInstance<NoContent>(Response<NoContent>.Success(200));
        }
    }
}
