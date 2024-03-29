﻿using FreeCourse.Web.Models.FakePayments;
using FreeCourse.Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class PaymentService : IPaymentService
    {
        public HttpClient httpClient { get; set; }

        public PaymentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
        {
            var response = await httpClient.PostAsJsonAsync<PaymentInfoInput>("FakePayment",paymentInfoInput);
            return response.IsSuccessStatusCode;
        }
    }
}
