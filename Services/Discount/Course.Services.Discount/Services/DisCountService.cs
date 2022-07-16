using Dapper;
using FreeCource.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Discount.Services
{
    public class DisCountService : IDiscountService
    {
        private readonly IConfiguration configuration;
        private readonly IDbConnection  dbConnection;

        public DisCountService(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbConnection = new NpgsqlConnection(configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int Id)
        {
            var status = await dbConnection.ExecuteAsync("delete from discount where id = @Id", new { Id = Id });
            if (status > 0)
                return Response<NoContent>.Success(status);
            return Response<NoContent>.Fail("Error while deleting", 500);
        }

        public async Task<Response<List<Model.Discount>>> GetAll()
        {
            var discounts = await dbConnection.QueryAsync<Model.Discount>("select * from discount");
            return Response<List<Model.Discount>>.Success(discounts.ToList(),200);
        }

        public async Task<Response<Model.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = (await dbConnection.QueryAsync<Model.Discount>("select * from discount where userid = @UserId AND code = @Code", new { Code = code, UserId = userId })).FirstOrDefault();
            if (discount == null)
                return Response<Model.Discount>.Fail("NotFound",404);
            return Response<Model.Discount>.Success(discount, 200);
        }

        public async Task<Response<Model.Discount>> GetById(int Id)
        {
            var discount = (await dbConnection.QueryAsync<Model.Discount>("select * from discount where id = @Id", new { Id })).FirstOrDefault();
            if(discount == null)
                return Response<Model.Discount>.Fail("Not Found!",404);
            return Response<Model.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Model.Discount discount)
        {
            var status = await dbConnection.ExecuteAsync("Insert into discount (userid,rate,code) values (@UserId,@Rate,@Code)",discount);
            if(status > 0)
                return Response<NoContent>.Success(status);
            return Response<NoContent>.Fail("an error accured while adding",500);
        }

        public async Task<Response<NoContent>> Update(Model.Discount discount)
        {
            var status = await dbConnection.ExecuteAsync("update discount set userid = @UserId,code = @Code, rate = @Rate where id = @Id", discount);
            if (status > 0)
                return Response<NoContent>.Success(status);
            return Response<NoContent>.Fail("Discount not found", 404);
        }
    }
}
