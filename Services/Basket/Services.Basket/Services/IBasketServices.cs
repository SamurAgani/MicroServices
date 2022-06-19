using FreeCource.Shared.Dtos;
using Services.Basket.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Basket.Services
{
    public interface IBasketServices
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> Delete(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
    }
}
