using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchoolECommerce.Order.Application.DTOs;

namespace UpSchoolECommerce.Order.Application.Mapping
{
    public class CustomMapping : Profile
    {
        //entity-dto eşleşmelerini yazacağımız mapleme işlemlerini yazalım.
        public CustomMapping()
        {
            //DTO ve entity'lerin eşleşmesini burada yazalım
            CreateMap<Domain.OrderAggregate.Order, OrderDTO>().ReverseMap();//tersine mapleme yapması için sonuna
                                                                            //.ReverseMap() ekliyoruz
            //Diğer entity-dto eşleşmelerini de yazalım
            CreateMap<Domain.OrderAggregate.OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<Domain.OrderAggregate.Address, AddressDTO>().ReverseMap();
        }
    }
}
