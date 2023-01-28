using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UpSchoolECommerce.Order.Application.DTOs;
using UpSchoolECommerce.Order.Application.Queries;
using UpSchoolECommerce.Order.Infrastructure;
using UpSchoolECommerce.Shared.Dtos;

namespace UpSchoolECommerce.Order.Application.Handlers
{
    public class GetOrderByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery,ResponseDto<List<OrderDTO>>>
    {
        //Handler sınıfı parametreyi tutan Query sınıfını ve DTO karşılığını alacak

        private readonly OrderDbContext _orderDbContext;
        private readonly IMapper _mapper;

        public GetOrderByUserIdQueryHandler(OrderDbContext orderDbContext, IMapper mapper)
        {
            _orderDbContext = orderDbContext;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<OrderDTO>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderDbContext.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();
            //BuyerId burada requestten UserId'ye eşit olacak

            //eğer sipariş varsa
            return ResponseDto<List<OrderDTO>>.Success(_mapper.Map<List<OrderDTO>>(orders), 200);


        }
    }
}
