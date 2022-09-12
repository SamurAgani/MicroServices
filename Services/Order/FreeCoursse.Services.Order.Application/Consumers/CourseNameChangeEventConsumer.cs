using FreeCource.Shared.Messages;
using FreeCourse.Services.Order.Infrastructure;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Consumers
{
    public class CourseNameChangeEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        public CourseNameChangeEventConsumer(OrderDbContext orderDbContext)
        {
            this.orderDbContext = orderDbContext;
        }

        public OrderDbContext orderDbContext { get; set; }
        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            var orderItems = orderDbContext.OrderItems.Where(x => x.ProductId == context.Message.CourseId).ToList();

            orderItems.ForEach(x => {
                x.UpdateOrderItem(context.Message.UpdatedName, x.ProductUrl, x.Price);
            
            });
            await orderDbContext.SaveChangesAsync();
        }
    }
}
