using FreeCource.Shared.Dtos;
using FreeCourse.Services.Order.Application.Command;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Domain.OrderAggregate;
using FreeCourse.Services.Order.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext context;
        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.Address.Province,request.Address.District,
                request.Address.Street, request.Address.ZipCode, request.Address.Line);
            var order = new Domain.OrderAggregate.Order(request.BuyerId,newAddress);
            request.OrderItems.ForEach(x =>
            {
                order.AddOrderItem(x.ProductId,x.ProductName,x.Price,x.ProductUrl);
            });
            await context.Orders.AddAsync(order);
            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = order.Id },200);
        }
        public CreateOrderCommandHandler(OrderDbContext context)
        {
            this.context = context;
        }
    }
}
