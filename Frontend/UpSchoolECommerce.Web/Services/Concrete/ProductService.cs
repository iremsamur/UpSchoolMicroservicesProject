using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UpSchoolECommerce.Shared.Dtos;
using UpSchoolECommerce.Web.Models.Catalogs;
using UpSchoolECommerce.Web.Services.Abstract;

namespace UpSchoolECommerce.Web.Services.Concrete
{
    public class ProductService : IProductService
    {
        //client ile mikroservisteki controller api metotlarına bağlanıyoruz
        private readonly HttpClient _client;

        public ProductService(HttpClient client)
        {
            _client = client;
        }
        //ekleme
        public async Task<bool> CreateProductAsync(ProductCreateViewModel productCreateViewModel)
        {
            var response = await _client.PostAsJsonAsync<ProductCreateViewModel>("Product",productCreateViewModel);//() içine istek yapılacak mikroservisteki controller adı belirtilir. Create işlemi için PostAsJsonAsync kullanıyorum
            return response.IsSuccessStatusCode;
        }
        //silme
        public async Task<bool> DeleteProductAsync(string ProductId)
        {
            var response = await _client.DeleteAsync($"Product/{ProductId}");//Controller adı Product veriliyor sonra / parametre Yani id'ye göre silme için id parametre oluyor
            return response.IsSuccessStatusCode;
        }
        //listeleme
        public async Task<List<ProductViewModel>> GetAllProductAsync()
        {
            var response = await _client.GetAsync("Product");
            if (!response.IsSuccessStatusCode)
            {
                return null;

            }
            var responseSucced = await response.Content.ReadFromJsonAsync<ResponseDto<List<ProductViewModel>>>();
            return responseSucced.Data;
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateViewModel productUpdateViewModel)
        {
            var response = await _client.PutAsJsonAsync<ProductUpdateViewModel>("Product", productUpdateViewModel);//güncellemede put metodu kullanılıyor.
            return response.IsSuccessStatusCode;
        }
    }
}
