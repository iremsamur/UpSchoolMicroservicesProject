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
        //ekleme işlemi
        public async Task<ResponseDto<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.InsertOneAsync(category);
            //InsertOneAsync async olarak tekil ekleme yapar. Birde InsertMany var oda çoğul ekleme yapar. Metot async olduğu için Async metot kullanılır.
            //Burada savechanges kullanılmıyor. InsertOneAsync mongoya değişiklikleri yansıtıyor.
            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);

        }
        //tüm verileri listeleme
        public async Task<ResponseDto<List<CategoryDto>>> GetAllAsync()
        {
            //tüm verileri listeleyecek metot
            var categories = await _categoryCollection.Find(category => true).ToListAsync();//find metodu bir parametre alır. Parametreye herhangi bir değil verilebilir.
            //Burada tamamını listelediğimiz için yani bir şart olmadığı için parametrenin ne olduğu çok önemli olmuyor. o yüzden category=>true verdik.
            return ResponseDto<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories),200);//yazdığımız Success metodunu burada çağırıyoruz.
            //entity ve dto eşleştirmesini yapabilmek için veritabanından getirdiğimiz categories ile sergilemmeyi sağlayacak CategoryDto'yu içine parametre vererek maplieyip eşleştiriyoruz
            //Success metodu içinde ResponseDto ile verileri ve durum kodu 200'ü gönderiyoruz.


        }
        //id'ye göre getirme
        public async Task<ResponseDto<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync(); //Category sınıfında çalış ve id değeri gönderilene eşit olana getir diyoruz.
            if (category == null)
            {
                return ResponseDto<CategoryDto>.Fail("Kategori bulunamadı.",404);//eğer o id'de bir kategori yoksa fail dönecek ve durum kodu 404 olur.

            }
            else
            {
                return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category),200);
                //veriyi getirirken her zaman mapleyerek getirir.
                //çünkü doğrudan entity!ler kullanılmaz. dto ile model eşleştirilerek kullanılır
            }
        }
    }
}
