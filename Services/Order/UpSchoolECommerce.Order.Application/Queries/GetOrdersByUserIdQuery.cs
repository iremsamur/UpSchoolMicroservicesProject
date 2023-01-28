using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UpSchoolECommerce.Order.Application.DTOs;
using UpSchoolECommerce.Shared.Dtos;

namespace UpSchoolECommerce.Order.Application.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<ResponseDto<List<OrderDTO>>>
    {
        //Burada Query'nin karşılığı ResponseDto<List> türünden dönen OrderDTO olacak
        //GetOrderByUserID metodunda kullanılacak id'ye göre getirme için parametreyi yazalım
        public string UserId { get; set; }
    }
}
