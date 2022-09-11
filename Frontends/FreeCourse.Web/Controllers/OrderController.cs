using FreeCourse.Web.Models.Orders;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService basketService;
        private readonly IOrderService orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            this.basketService = basketService;
            this.orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await basketService.Get();
            ViewBag.basket = basket;
            return View(new CheckoutInfoInput());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput input)
        {
            // senxron
            //var orderStatus = await orderService.CreateOrder(input);
            // asinxron
            var orderStatus = await orderService.SuspendOrder(input);
            if (!orderStatus.IsSuccess)
            {
                var basket = await basketService.Get();
                ViewBag.basket = basket;
                ViewBag.error = orderStatus.Error;
                return View();
            }
            // senxron
            //return RedirectToAction("SuccessfulCheckout",new { orderId = orderStatus.OrderId });
            //asinxron
            return RedirectToAction("SuccessfulCheckout",new { orderId = new Random().Next(1,1000) });
        }

        public IActionResult SuccessfulCheckout(int orderId)
        {
            ViewBag.orderId = orderId;
            return View();
        }

        public async Task<IActionResult> CheckOutHistory()
        {
            return View(await orderService.GetOreder());
        }
    }
}
