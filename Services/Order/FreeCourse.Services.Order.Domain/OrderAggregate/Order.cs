using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class Order : Entity,IAggregateRoot
    {
        public DateTime CreatedDate { get; set; }
        public Address Address { get; set; }
        public string BuyerId { get; set; }

      //  private List<OrderItem> _orderItem;
        public List<OrderItem> orderItems { get; set; }

        public Order()
        {

        }
        public Order(string buyerId,Address address)
        {
            orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            BuyerId = buyerId;
            Address = address;
        }
        public decimal GetTotalPrice => orderItems.Sum(x => x.Price);
        public void AddOrderItem(string productId,string productName,decimal price, string pictureUrl)
        {
            var existProduct = orderItems.Any(x => x.ProductId == productId);
            if (!existProduct)
            {
                orderItems.Add(new OrderItem(productId, productName, pictureUrl, price));
            }
        }
    }
}
