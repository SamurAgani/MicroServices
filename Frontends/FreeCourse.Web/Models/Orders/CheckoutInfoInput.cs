using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Orders
{
    public class CheckoutInfoInput
    {
        public string Province { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        [Display(Name = "Adress")]
        public string Line { get; set; }


        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public decimal Price { get; set; }

    }
}
