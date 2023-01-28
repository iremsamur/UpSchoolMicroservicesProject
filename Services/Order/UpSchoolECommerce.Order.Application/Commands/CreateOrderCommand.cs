using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchoolECommerce.Order.Application.DTOs;
using UpSchoolECommerce.Order.Domain.OrderAggregate;
using UpSchoolECommerce.Shared.Dtos;

namespace UpSchoolECommerce.Order.Application.Commands
{
    public class CreateOrderCommand: IRequest<ResponseDto<CreatedOrderDTO>>
    {
        //order ekleme için parametreleri tutacak Command sınıfını oluşturalım. Bunun türü de CreateOrderDTO oluyor
        public string BuyerId { get; set; }
        public AddressDTO Address { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
