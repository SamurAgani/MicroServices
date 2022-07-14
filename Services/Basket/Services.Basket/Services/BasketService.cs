using FreeCource.Shared.Dtos;
using Services.Basket.DTOS;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Basket.Services
{
    public class BasketService : IBasketServices
    {
        private readonly RedisService redisService;

        public BasketService(RedisService redisService)
        {
            this.redisService = redisService;
        }

        public async Task<Response<bool>> Delete(string userId)
        {
            var status = await redisService.GetDb().KeyDeleteAsync(userId);
            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Error while deleting", 404);
        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            var basketExist = await redisService.GetDb().StringGetAsync(userId);
            if (string.IsNullOrEmpty(basketExist))
            {
                return Response<BasketDto>.Fail("Basket not found!", 404);
            }
             
            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(basketExist), 200);
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
            return status ? Response<bool>.Success(200) : Response<bool>.Fail("Error while saving", 500);
        }
    }
}
