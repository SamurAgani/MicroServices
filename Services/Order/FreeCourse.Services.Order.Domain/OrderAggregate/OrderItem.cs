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

        public string ProductId { get;  set; }
        public string ProductName { get;  set; }
        public string ProductUrl { get;  set; }
        public Decimal Price { get;  set; }

        public void UpdateOrderItem( string productName, string productUrl, decimal price)
        {
            ProductName = productName;
            ProductUrl = productUrl;
            Price = price;
        }
    }
}
