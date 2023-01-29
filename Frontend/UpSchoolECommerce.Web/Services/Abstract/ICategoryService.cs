using System.Collections.Generic;
using System.Threading.Tasks;
using UpSchoolECommerce.Web.Models.Catalogs;

namespace UpSchoolECommerce.Web.Services.Abstract
{
    public interface ICategoryService
    {
        //Kategorileri listeleme
        Task<List<CategoryViewModel>> GetAllCategoryAsync();
    }
}
