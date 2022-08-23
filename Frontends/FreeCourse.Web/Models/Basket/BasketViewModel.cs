using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeCourse.Web.Models.Basket
{
    public class BasketViewModel
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public int? DiscountRate { get; set; }


        public List<BasketItemViewModel> basketItems { get; set; }

        public List<BasketItemViewModel> BasketItems
        {
            get
            {
                if (HasDiscount)
                {
                    basketItems.ForEach(x =>
                    {
                        var discountPrice = x.Price * ((decimal)DiscountRate.Value / 100);
                        x.AppliedDiscount(Math.Round(x.Price - discountPrice, 2));
                    });
                }
                return basketItems;

            }
            set
            {
                basketItems = value;
            }
        }
        public decimal TotalPrice { get => basketItems.Sum(x=>x.GetCurrentPrice); }

        public bool HasDiscount
        {
            get=> !string.IsNullOrEmpty(DiscountCode);
        }
    }
}
