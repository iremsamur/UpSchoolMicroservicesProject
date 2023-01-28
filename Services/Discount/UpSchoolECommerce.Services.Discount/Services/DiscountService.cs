using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UpSchoolECommerce.Shared.Dtos;

namespace UpSchoolECommerce.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));//postgresql bağlantısı
            //Bu fonksiyon GetConnectionString parametre olarak appsettings.json içinde tanımlanan key'in ismini alır.
        }
        //şimdi dapper kullanalım. dapper'da ado.net gibi sorgular olacak
        public async Task<ResponseDto<NoContent>> Delete(int id)
        {
            //dapper+postgresql kodları
            //dapper veritabanları ile senkron bir şekilde çalışan bir orm'dir.
            var status = await _dbConnection.ExecuteAsync("delete from discount where id=@id", new
            {
                id = id
            });
            //eğer silinecek eleman varsa
            return status > 0 ? ResponseDto<NoContent>.Success(204) : ResponseDto<NoContent>.Fail("Silinecek değer bulunamadı.",404); 
        }

        public async Task<ResponseDto<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from discount");
            return ResponseDto<List<Models.Discount>>.Success(discounts.ToList(), 200);

        }

        public Task<ResponseDto<List<Models.Discount>>> GetByCodeUserID(string code, string userID)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseDto<Models.Discount>> GetByID(int id)
        {
            var discounts = (await _dbConnection.QueryAsync<Models.Discount>("select * from discount where id=@id", new
            {
                id=id
            })).SingleOrDefault();

            return ResponseDto<Models.Discount>.Success(discounts, 200);
        }

        public async Task<ResponseDto<NoContent>> Save(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("insert into discount (userid,rate,code) values (@UserID,@Rate,@Code)",discount);//bu executeasync ado.net'deki executenonquery'nin karşılığı
            //values değerlerine @'li olarak propları tanımladığım şekliyle gönderiyorum. discount vererek parametrem modelden gelen değerler @'lerin karşılığı olacak. Aynı isimde olduğu için.
            if (status > 0)
            {
                return ResponseDto<NoContent>.Success(204);
            }
            return ResponseDto<NoContent>.Fail("Bir hata oluştu.", 500);

        }

        public async Task<ResponseDto<NoContent>> Update(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("update discount set userid=@UserID,rate=@Rate,code=@Code where id=@id", discount);
            if (status > 0)
            {
                return ResponseDto<NoContent>.Success(204);
            }
            return ResponseDto<NoContent>.Fail("Bir hata oluştu.", 500);
        }
    }
}
