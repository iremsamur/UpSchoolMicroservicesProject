using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UpSchoolECommerce.Services.Catalog.Dtos;
using UpSchoolECommerce.Services.Catalog.Services.Abstract;
using UpSchoolECommerce.Shared.ControllerBases;

namespace UpSchoolECommerce.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustomBaseController
    {
        //Bu sınıf yazdığımız CustomeBaseController'dan miras alıyor. 
        //CustomeBase'den BaseController'dan miras alıyordu


        //servisi çağırıyoruz.
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //listeleme
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            return CreateActionResultInstance(response);
            //custombase içinde yer alan metodu çağırarak response'u ona gönderiyoruz böylece return ok() gibi döneceği cevapları burada yazmaktan kurtuluyoruz
        }

        //localhost:5011/api/category/GetById şeklinde istek atılacak
        //atılan isteğe göre uygun api karşılayacak
        //id'ye göre veri getirme
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }

        //ekleme işlemi
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            var response = await _categoryService.CreateAsync(categoryDto);
            return CreateActionResultInstance(response);
        }


        

    }
}
