using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using UpSchoolECommerce.IdentityServer.DTOs;
using UpSchoolECommerce.IdentityServer.Models;
using UpSchoolECommerce.Shared.Dtos;
using static IdentityServer4.IdentityServerConstants;

namespace UpSchoolECommerce.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]//bunu sayfaya yetki erişimini düzenleyebilmek için ekledik
    [Route("api/[controller]/[action]")]//buraya controller sonrasına [action] ekleyerek metot isimlerini alır. Yani postman üzerinde bu apiye istek yaparken metot ismiyle istek yapabileceğim. Bu action'ı eklemeseydim sadece Controller sınıf adı ile istek atabilir olacaktım.
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        //Burada get post olarak iki metot olmayacak çünkü 
        //API controller UI değil. tasarım yok
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDTO signUpDTO)
        {
            var user = new ApplicationUser
            {
                UserName = signUpDTO.UserName,
                Email = signUpDTO.Email,
                City = signUpDTO.City
            };
            var result = await _userManager.CreateAsync(user, signUpDTO.Password);
            if (!result.Succeeded)
            {
                //eğer başarılı değilse
                return BadRequest(ResponseDto<NoContent>.Fail(result.Errors.Select(x=>x.Description).ToList(),400));//400 hata kodu

            }
            return NoContent();

        }
    }
}
