using System.Threading.Tasks;

namespace UpSchoolECommerce.Web.Services.Abstract
{
    public interface IClientCredentialTokenService
    {
        Task<string> GetToken();
    }
}
