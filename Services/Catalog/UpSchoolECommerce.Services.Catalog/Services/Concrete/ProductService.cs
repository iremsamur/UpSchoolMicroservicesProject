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
        public async Task<ResponseDto<ProductDto>> CreateAsync(CreateProductDto createProductDto)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseDto<NoContent>> DeleteAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseDto<ProductDto>> GetByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseDto<NoContent>> UpdateAsync(UpdateProductDto updateProductDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
