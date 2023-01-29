using Microsoft.AspNetCore.Http;

namespace UpSchoolECommerce.Web.Models.Catalogs
{
    public class ProductCreateViewModel
    {
     
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }

        public string Image { get; set; }

        public string CategoryId { get; set; }
        public IFormFile FormFile{ get; set; }

    }
}
