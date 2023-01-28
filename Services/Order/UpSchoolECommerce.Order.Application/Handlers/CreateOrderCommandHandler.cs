using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UpSchoolECommerce.Order.Application.Commands;
using UpSchoolECommerce.Order.Application.DTOs;
using UpSchoolECommerce.Order.Domain.OrderAggregate;
using UpSchoolECommerce.Order.Infrastructure;
using UpSchoolECommerce.Shared.Dtos;

namespace UpSchoolECommerce.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ResponseDto<CreatedOrderDTO>>
    {
        private readonly OrderDbContext _orderDbContext;

        public CreateOrderCommandHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<ResponseDto<CreatedOrderDTO>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.Address.City, request.Address.Street,request.Address.District, request.Address.ZipCode);
            //Burada sıraları önemli
            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(request.BuyerId, newAddress);
            request.OrderItems.ForEach(x =>
            {
                //AddOrderItem Siparişin içine tek tek o siparişe ait değerleri eklememizi sağlıyor.
                newOrder.AddOrderItem(x.ProductId, x.Name, x.Price, x.PictureURL);
            });
            await _orderDbContext.Orders.AddAsync(newOrder);
            await _orderDbContext.SaveChangesAsync();
            return ResponseDto<CreatedOrderDTO>.Success(new CreatedOrderDTO
            {
                OrderId = newOrder.Id
            }, 204);

        }
    }
}
