using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSchoolECommerce.Order.Application.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
     
        public DateTime CreatedDate { get; set; }

        //ilişkiden gelen Address için Burada AddressDTO eklenecek
        public AddressDTO Address { get; set; }


        public string BuyerId { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }//yine ilişkili OrderItems'dan gelen liste list dto olarak burada tanımlanıyor
    }
}
