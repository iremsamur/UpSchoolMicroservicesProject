using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using UpSchoolECommerce.IdentityServer.Models;

namespace UpSchoolECommerce.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        //Burada kullanıcı sisteme giriş yaparken kullanıcıda kullanıcı adı ve şifresi doğru mu kontrol edilecek. 

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existUser = await _userManager.FindByEmailAsync(context.UserName);
            if (existUser == null)
            {
                //eğer öyle bir kullanıcı yoksa

                var errors = new Dictionary<string, object>();
                errors.Add("errors", "email adresiniz veya şifreniz yanlıştır.");//ilk parametre string key, ikinci value string olur.
                context.Result.CustomResponse = errors;
            }
            var passwordCheck = await _userManager.CheckPasswordAsync(existUser, context.Password);
            if (passwordCheck == false)
            {
                //eğer şifre yanlışsa
                var errors = new Dictionary<string, object>();
                errors.Add("errors", "email adresiniz veya şifreniz yanlıştır.");//ilk parametre string key, ikinci value string olur.
                context.Result.CustomResponse = errors;

            }
            //eğer şifre ya da password yukarıdakilere takılmazsa
            context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}
