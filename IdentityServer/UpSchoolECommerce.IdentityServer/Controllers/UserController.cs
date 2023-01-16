using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UpSchoolECommerce.IdentityServer.DTOs;
using UpSchoolECommerce.IdentityServer.Models;


namespace UpSchoolECommerce.IdentityServer.Controllers
{
    [Route("api/[controller]")]
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
                return BadRequest();//400 hata kodu

            }
            return NoContent();

        }
    }
}
