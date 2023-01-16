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
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        //mongo db işlemleri için IMongoCollection'da kullanılır. Veritabanı işlemi olduğu için entity olan Category'i alır.
        private readonly IMapper _mapper;//mapleme için mapper'ı tanımlıyorum.

        public CategoryService(IDatabaseSettings databaseSettings,IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);//IDatabaseSettings'den veritabanı bağlantı adresini alıyorum.
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);//Burada da IDatabaseSettings'den c#'daki CategoryCollextionName tablosunu Category entity'si eşleştirip aldım
            //mapper'ıda kullanalım
            _mapper = mapper;
            
        }

        public async Task<ResponseDto<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseDto<List<CategoryDto>>> GetAllASync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseDto<CategoryDto>> GetByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
