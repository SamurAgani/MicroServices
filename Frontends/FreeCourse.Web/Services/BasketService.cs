﻿using FreeCourse.Web.Models.Basket;
using FreeCourse.Web.Services.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient httpClient;

        public BasketService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task AddBasketItem(BasketItemViewModel basketItemViewModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ApplyDiscount(string discountCode)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CancelApplyDiscount()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete()
        {
            throw new System.NotImplementedException();
        }

        public Task<BasketItemViewModel> Get()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveBasketItem(string courseId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveOrUpdate(BasketViewModel basketViewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}