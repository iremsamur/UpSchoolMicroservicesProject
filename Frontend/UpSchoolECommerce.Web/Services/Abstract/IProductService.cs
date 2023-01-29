using System.Collections.Generic;
using System.Threading.Tasks;
using UpSchoolECommerce.Web.Models.Catalogs;

namespace UpSchoolECommerce.Web.Services.Abstract
{
    public interface IProductService
    {
        //controller'da bu metotlar çağırılacak
        Task<List<ProductViewModel>> GetAllProductAsync();
        Task<bool> CreateProductAsync(ProductCreateViewModel productCreateViewModel);

        Task<bool> UpdateProductAsync(ProductUpdateViewModel productUpdateViewModel);

        Task<bool> DeleteProductAsync(string ProductId);


    }
}
