using System;

namespace UpSchoolECommerce.Services.Discount.Models
{
    [Dapper.Contrib.Extensions.Table("discount")] //postgresql 'de küçük harf kullanılır. o yüzden tablo küçük harfle yazılır.
    public class Discount
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public DateTime CreatedTime{ get; set; }
    }
}
