namespace UpSchoolECommerce.Services.Catalog.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {

        //property'leri implemente ettim. 
        public string ProductCollectionName { get; set ; }
        public string CategoryCollectionName { get; set ; }
        public string ConnectionString { get ; set ; }
        public string DatabaseName { get; set; }
    }
}
