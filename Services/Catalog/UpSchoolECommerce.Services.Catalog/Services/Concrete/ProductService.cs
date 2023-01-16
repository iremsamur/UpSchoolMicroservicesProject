using AutoMapper;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using UpSchoolECommerce.Services.Catalog.Dtos;
using UpSchoolECommerce.Services.Catalog.Models;
using UpSchoolECommerce.Services.Catalog.Services.Abstract;
using UpSchoolECommerce.Services.Catalog.Settings;
using UpSchoolECommerce.Shared.Dtos;

namespace UpSchoolECommerce.Services.Catalog.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        //mongo db işlemleri için IMongoCollection'da kullanılır. Veritabanı işlemi olduğu için entity olan Category'i alır.
        private readonly IMapper _mapper;//mapleme için mapper'ı tanımlıyorum.

        public ProductService(IDatabaseSettings databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);//IDatabaseSettings'den veritabanı bağlantı adresini alıyorum.
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);//productcollection tablosunun mongodb karşılığını alır.
            //mapper'ıda kullanalım
            _mapper = mapper;

        }
        //ürün ekleme
        public async Task<ResponseDto<ProductDto>> CreateAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);//productdto'yu entity olan product ile eşleştirip ekleme yapacak
            await _productCollection.InsertOneAsync(product);
            return ResponseDto<ProductDto>.Success(_mapper.Map<ProductDto>(product), 200); //datanın eklenmiş halini yanıtın sonucunu görmek için productdto'yu parametre olarak alır.
            //yine dto ve entity'i bağlamak için mapper ile maplenerek veri gönderiliyor
        }

        //silme işlemi
        public async Task<ResponseDto<NoContent>> DeleteAsync(string id)
        {
            var result = await _productCollection.DeleteOneAsync(x => x.Id == id);//DeleteOneAsync içindeki id'ye göre silme yapar.
            if (result.DeletedCount > 0)
            {
                //silinen eleman sayısı>0 ise yani silme işlemi başarılı ise
                return ResponseDto<NoContent>.Success(204);
                    //silinen kaydı bana tekrar göstermesine gerek yok bu sebeple NoContent kullanıyorum.
                    //Bu kez durum kodu 204 olur yani işlem başarılı ama geriye dönen bir içerik yok demektir.

            }
            else
            {
                return ResponseDto<NoContent>.Fail("Silinecek ürün bulunamadı.",404);//eğer başarısız olursa hata mesajı verecek fail overload'u kullanalım.
            }
        }

        //listeleme işlemi
        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            var products = await _productCollection.Find(product => true).ToListAsync();
            return ResponseDto<List<ProductDto>>.Success(_mapper.Map<List<ProductDto>>(products), 200);

        }
        //id'ye göre veri getirme
        public async Task<ResponseDto<ProductDto>> GetByIdAsync(string id)
        {
            var products = await _productCollection.Find<Product>(x => x.Id == id).FirstOrDefaultAsync();
            if (products == null)
            {
                return ResponseDto<ProductDto>.Fail("Girilen ID'ye ait  bir ürün bulunamadı.",404);
            }
            else
            {
                return ResponseDto<ProductDto>.Success(_mapper.Map<ProductDto>(products), 200);

            }

        }

        //güncelleme işlemi
        public async Task<ResponseDto<NoContent>> UpdateAsync(UpdateProductDto updateProductDto)
        {
            var updatedProduct = _mapper.Map<Product>(updateProductDto);
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.Id == updateProductDto.Id,updatedProduct);
            //ilk parametre neye göre güncellenecek ikincisi ise güncellenmiş yeni verilerin olduğu hali

            if (result == null)
            {
                return ResponseDto<NoContent>.Fail("Güncellenecek ürün bulunamadı", 404);
            }
            else
            {
                return ResponseDto<NoContent>.Success(204);
            }


        }
    }
}
