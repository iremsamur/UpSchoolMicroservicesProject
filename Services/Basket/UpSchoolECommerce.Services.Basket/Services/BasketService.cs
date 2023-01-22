
using System.Text.Json;
using System.Threading.Tasks;
using UpSchoolECommerce.Services.Basket.Dtos;
using UpSchoolECommerce.Shared.Dtos;

namespace UpSchoolECommerce.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<ResponseDto<bool>> Delete(string userId)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(userId);
            return status ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Bir hata oluştu.", 404);

        }

        public async Task<ResponseDto<BasketDto>> GetBasket(string userId)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(userId);
            if (string.IsNullOrEmpty(existBasket))
            {
                //exisBasket null ise
                return ResponseDto<BasketDto>.Fail("Sepet bulunamadı.", 404);
            }
            //Burada gelen veri json formatında, bunu listelerken deserialize ederek veririm.
            return ResponseDto<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);
        }

        public async Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));

            return status ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Bir hata oluştu.", 500);
        }
    }
}
