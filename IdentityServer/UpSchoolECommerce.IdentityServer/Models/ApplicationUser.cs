using Microsoft.AspNetCore.Identity;

namespace UpSchoolECommerce.IdentityServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //Buraya ek sütunlar ekleyebiliriz. AppUser sınııfna karşılık gelir.
        public string City { get; set; }
    }
}
