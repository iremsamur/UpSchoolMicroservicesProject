using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSchoolECommerce.Order.Application.DTOs
{
    public class OrderItemDTO
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string PictureURL { get; set; }
        public decimal Price { get; set; }
    }
}
