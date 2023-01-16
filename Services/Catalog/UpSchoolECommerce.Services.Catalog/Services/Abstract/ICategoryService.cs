using System.Collections.Generic;
using System.Threading.Tasks;
using UpSchoolECommerce.Services.Catalog.Dtos;
using UpSchoolECommerce.Shared.Dtos;

namespace UpSchoolECommerce.Services.Catalog.Services.Abstract
{
    public interface ICategoryService
    {
        //Kategori işlemleri metotlarını tutacak
        //kategorilerin tamamını listeleyen metot
        //dönen response'a göre cevap gönderecek metotları çağırmak için 
        //ResponseDto dönüyor. Bu ResponseDto generic olduğu için tüm veirleri getireceği için list türünde ve yazdığımız CategoryDto türünde dönüyor
        Task<ResponseDto<List<CategoryDto>>> GetAllAsync();

        //ekleme işlemi
        Task<ResponseDto<CategoryDto>> CreateAsync(CategoryDto categoryDto);//modeli burada kullanmıyoruz. Verileri dto'dan entity'e tşaıyacak o yüzden listeleme veya eklenecek veriler
        //dto üzerinde taşınır 

        Task<ResponseDto<CategoryDto>> GetByIdAsync(string id);//id'ye göre veri getirme



    }
}
