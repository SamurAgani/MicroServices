using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class OrderItem : Entity
    {
        public OrderItem(string productId, string productName, string productUrl, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            ProductUrl = productUrl;
            Price = price;
        }
        public OrderItem()
        {

        }

        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ProductUrl { get; private set; }
        public Decimal Price { get; private set; }

        public void UpdateOrderItem(string productId, string productName, string productUrl, decimal price)
        {
            ProductName = productName;
            ProductUrl = productUrl;
            Price = price;
        }
    }
}
