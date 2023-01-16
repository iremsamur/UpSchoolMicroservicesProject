using System.Collections.Generic;
using System.Threading.Tasks;
using UpSchoolECommerce.Services.Catalog.Dtos;
using UpSchoolECommerce.Shared.Dtos;

namespace UpSchoolECommerce.Services.Catalog.Services.Abstract
{
    public interface IProductService
    {
        Task<ResponseDto<List<ProductDto>>> GetAllAsync();
        Task<ResponseDto<ProductDto>> GetByIdAsync(string id);
        Task<ResponseDto<ProductDto>> CreateAsync(CreateProductDto createProductDto);
        //Burada Response ekleme sonucu verileri getireceği için Response'un kaynağı ProductDto olur ama ekleme işlemi için eklenecek parametreleri tutan CreateProductDto olur.

        //güncelleme işlemi
        //güncelleme ve silme işlemlerinde hiçbir şey dönmeyecek o yüzden NoContent olur.
        Task<ResponseDto<NoContent>> UpdateAsync(UpdateProductDto updateProductDto);

        //silme işlemi
        Task<ResponseDto<NoContent>> DeleteAsync(string id);

    }
}
