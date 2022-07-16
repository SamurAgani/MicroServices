using FreeCource.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<List<Model.Discount>>> GetAll();
        Task<Response<Model.Discount>> GetById(int Id);
        Task<Response<NoContent>> Save(Model.Discount discount);
        Task<Response<NoContent>> Update(Model.Discount discount);
        Task<Response<NoContent>> Delete(int Id);
        Task<Response<Model.Discount>> GetByCodeAndUserId(string code, string userId);


    }
}
