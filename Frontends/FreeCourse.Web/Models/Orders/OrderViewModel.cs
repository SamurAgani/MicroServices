using System;
using System.Collections.Generic;

namespace FreeCourse.Web.Models.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; private set; }
       // public AddressDto Address { get; private set; }
        public string BuyerId { get; private set; }
        public List<OrderItemViewModel> orderItems;
    }
}
