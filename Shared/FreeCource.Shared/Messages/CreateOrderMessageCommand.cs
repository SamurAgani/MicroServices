using System;
using System.Collections.Generic;
using System.Text;

namespace FreeCource.Shared.Messages
{
    public class CreateOrderMessageCommand
    {
        public string Province { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string Line { get; set; }
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }

    public class OrderItemDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public Decimal Price { get; set; }
    }
}
