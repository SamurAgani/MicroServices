using FreeCourse.Web.Models.Orders;
using FreeCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class OrderService : IOrderService
    {
        public Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            throw new System.NotImplementedException();
        }

        public Task GetOrder()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<OrderViewModel>> GetOreder()
        {
            throw new System.NotImplementedException();
        }

        public Task SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            throw new System.NotImplementedException();
        }
    }
}
