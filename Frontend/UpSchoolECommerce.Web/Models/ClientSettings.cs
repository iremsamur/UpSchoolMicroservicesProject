namespace UpSchoolECommerce.Web.Models
{
    public class ClientSettings
    {
        public Client WebClient { get; set; }
        public Client MvcClientForUser { get; set; }//bu isimler Identity Config içindeki client name'leri ile aynı olacak
        public Client MvcClient{ get; set; }

    }
    public class Client
    {
        //bunların json dosyasında değer karşılığı olacak
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
