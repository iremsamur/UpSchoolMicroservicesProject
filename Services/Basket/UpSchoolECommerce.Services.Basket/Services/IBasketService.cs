using System.Threading.Tasks;
using UpSchoolECommerce.Services.Basket.Dtos;
using UpSchoolECommerce.Shared.Dtos;

namespace UpSchoolECommerce.Services.Basket.Services
{
    public interface IBasketService
    {
        //sepet işlemleri
        Task<ResponseDto<BasketDto>> GetBasket(string userId);
        Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<ResponseDto<bool>> Delete(string userId);

    }
}
