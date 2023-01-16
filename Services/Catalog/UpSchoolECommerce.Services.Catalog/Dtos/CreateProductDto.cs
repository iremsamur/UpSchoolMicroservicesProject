using UpSchoolECommerce.Services.Catalog.Models;

namespace UpSchoolECommerce.Services.Catalog.Dtos
{
    public class CreateProductDto
    {
       
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }

        public string Image { get; set; }

        public string CategoryId { get; set; }

  
    }
}
