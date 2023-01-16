namespace UpSchoolECommerce.Services.Catalog.Settings
{
    public interface IDatabaseSettings
    {
        //appsettings.json içinde yer alan DatabaseSettings attribute içindeki tüm tanımladığım parametreleri burada yazıyorum
        public string ProductCollectionName { get; set; }//product tablo adı parametresi
        public string CategoryCollectionName { get; set; }//kategorisi tablosu adı parametresi
        public string ConnectionString { get; set; }//veritabanı bağlantı adresi
        public string DatabaseName { get; set; }//veritabanı adı


    }
}
