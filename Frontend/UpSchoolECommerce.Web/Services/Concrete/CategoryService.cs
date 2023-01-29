using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UpSchoolECommerce.Shared.Dtos;
using UpSchoolECommerce.Web.Models.Catalogs;
using UpSchoolECommerce.Web.Services.Abstract;

namespace UpSchoolECommerce.Web.Services.Concrete
{
    public class CategoryService : ICategoryService
    {

        private readonly HttpClient _client;

        public CategoryService(HttpClient client)
        {
            _client = client;
        }

        //Category api mikroservisindeki metodu çağırıyoruz
        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _client.GetAsync("Category");
            if (!response.IsSuccessStatusCode)
            {
                return null;

            }
            var responseSucced = await response.Content.ReadFromJsonAsync<ResponseDto<List<CategoryViewModel>>>();
            return responseSucced.Data;

        }

        
    }
}
