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
    public class ProductController : CustomBaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

      
        //listeleme işlemi
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        //id'ye göre getirme
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            var response = await _productService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }
        //ekleme
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto createProductDto)
        {
            var response = await _productService.CreateAsync(createProductDto);
            return CreateActionResultInstance(response);
        }
        //güncelleme
        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductDto updateProductDto)
        {
            var response = await _productService.UpdateAsync(updateProductDto);
            return CreateActionResultInstance(response);
        }
        //silme işlemi
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _productService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }




    }
}
