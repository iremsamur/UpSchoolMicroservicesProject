namespace UpSchoolECommerce.Web.Models
{
    public class ServicesApiSettings
    {
        public string IdentityBaseUrl { get; set; }
        public string GatewayBaseUrl { get; set; }
        public string PhotoStockUrl { get; set; }
        //Bunun içine ServicesApi türünde tüm mikroservisler yazılacak
        //Arka tarafta bunlarla mikroservislerin yollarını alıp bağlantı atmak amacımız olacak
        public ServicesApi Catalog { get; set; }
        public ServicesApi Basket { get; set; }
        public ServicesApi Discount { get; set; }
        public ServicesApi FakePayment { get; set; }
        public ServicesApi Order { get; set; }

    }
    public class ServicesApi
    {
        public string Path { get; set; }//mikroservisin konumu url'i
       

    }
}
