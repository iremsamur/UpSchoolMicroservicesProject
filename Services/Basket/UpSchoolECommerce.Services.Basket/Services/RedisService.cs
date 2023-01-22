using StackExchange.Redis;

namespace UpSchoolECommerce.Services.Basket.Services
{
    public class RedisService
    {
        private readonly string _host;
        private readonly int _port;
        private ConnectionMultiplexer _connectionMultiplexer; //ConnectionMultiplexer sınıfı Redis'e bağlanmamızı sağlayan sınıf

        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        //Redis ile bağlantı kuracak metodu yazalım.
        //Bağlantıyı gönderdiğim host ve porttan almasını sağlar.
        public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");


        public IDatabase GetDb(int db = 1) => _connectionMultiplexer.GetDatabase(db);
        //redis ile varsayılanda geliştirme, production ve test gibi çeşitli
        //veritabanları geliyor 1 ile geliştirme ortamı için database seçmiş oluyoruz.
        //birinci sıradaki veritabanına bana ayır dedik.
    }
}
