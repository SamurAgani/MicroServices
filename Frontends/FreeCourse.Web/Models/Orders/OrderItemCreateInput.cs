using System;

namespace FreeCourse.Web.Models.Orders
{
    public class OrderItemCreateInput
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public Decimal Price { get; set; }
    }
}
