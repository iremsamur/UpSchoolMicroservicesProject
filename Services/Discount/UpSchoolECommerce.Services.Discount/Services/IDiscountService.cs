using System.Collections.Generic;
using System.Threading.Tasks;
using UpSchoolECommerce.Shared.Dtos;

namespace UpSchoolECommerce.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<ResponseDto<List<Models.Discount>>> GetAll();

        Task<ResponseDto<Models.Discount>> GetByID(int id);

        Task<ResponseDto<NoContent>> Save(Models.Discount discount);

        Task<ResponseDto<NoContent>> Update(Models.Discount discount);

        Task<ResponseDto<NoContent>> Delete(int id);

        Task<ResponseDto<List<Models.Discount>>> GetByCodeUserID(string code, string userID);//parametreye göre kod getir
    }
}
