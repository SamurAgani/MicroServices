﻿using FreeCource.Shared.Dtos;
using FreeCource.Shared.Services;
using FreeCourse.Web.Models.FakePayments;
using FreeCourse.Web.Models.Orders;
using FreeCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class OrderService : IOrderService
    {
        public readonly IPaymentService  paymentService;
        public readonly HttpClient httpClient;
        public readonly IBasketService basketService;
        public ISharedIdentityService sharedIdentityService;

        public OrderService(IPaymentService paymentService, HttpClient httpClient, IBasketService basketService)
        {
            this.paymentService = paymentService;
            this.httpClient = httpClient;
            this.basketService = basketService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await basketService.Get();
            var payment = new PaymentInfoInput();
            payment.CVV = checkoutInfoInput.CVV;
            payment.Expiration = checkoutInfoInput.Expiration;
            payment.CardNumber = checkoutInfoInput.CardNumber;
            payment.CardName = checkoutInfoInput.CardName;
            payment.TotalPrice = basket.TotalPrice;

            var responsePayment = await paymentService.ReceivePayment(payment);
            if (!responsePayment)
            {
                return new OrderCreatedViewModel() { Error = "Payment error", IsSuccess = false };
            }
            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = sharedIdentityService.GetUserId,
                Address = new AddressCreateInput() { Province = checkoutInfoInput.Province, District = checkoutInfoInput.District,
                                                    Line = checkoutInfoInput.Line, Street = checkoutInfoInput.Street, ZipCode = checkoutInfoInput.ZipCode}
            };

            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new OrderItemCreateInput { Price = x.Price, ProductId = x.CourseId, ProductName = x.CourseName, ProductUrl = "" };
                orderCreateInput.OrderItems.Add(orderItem);
            });
            var response = await httpClient.PostAsJsonAsync<OrderCreateInput>("orders",orderCreateInput);
            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel() { Error = "Payment error", IsSuccess = false };
            }
            var orderCreated = await response.Content.ReadFromJsonAsync<OrderCreatedViewModel>();
            return orderCreated;
        }

        public async Task GetOrder()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<OrderViewModel>> GetOreder()
        {
            var response = await httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");
            return response.Data;
        }

        public Task SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            throw new System.NotImplementedException();
        }
    }
}
