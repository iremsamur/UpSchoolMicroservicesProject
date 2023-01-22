using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using UpSchoolECommerce.Services.PhotoStock.DTOs;
using UpSchoolECommerce.Shared.ControllerBases;
using UpSchoolECommerce.Shared.Dtos;

namespace UpSchoolECommerce.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile formFile)
        {
            if (formFile != null && formFile.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos");
                var stream = new FileStream(path,FileMode.Create);
                await formFile.CopyToAsync(stream);//cancellationtoken parametresini de alabilir. Bununla bazen işlem uzun sürdüğünde o sayfayı kullanıcı kapatırsa işlem sona erer.
                var returnPath = "photos/"+formFile.FileName;
                PhotoDto photoDto = new()
                {
                    URL = returnPath//eklenen dosyanın bilgisini returnPath tutuyor.
                };
                return CreateActionResultInstance(ResponseDto<PhotoDto>.Success(photoDto,200));//Eğer işlem başarılı ise eklenen resmin bilgilerini dönsün.

            }
            return CreateActionResultInstance(ResponseDto<PhotoDto>.Fail("Bir hata oluştu.", 400));
        }
    }
}
