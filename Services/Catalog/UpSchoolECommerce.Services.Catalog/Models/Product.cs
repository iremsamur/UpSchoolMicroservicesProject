using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UpSchoolECommerce.Services.Catalog.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]//BsonRepresentation alanın tipini belirliyor.
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public string Image { get; set; }

        //Mongodb veritabanı kullandığım için 
        //ilişkisi şu şekilde oluştururuz.
        [BsonRepresentation(BsonType.ObjectId)]//yine id alanı unique
        public string CategoryId { get; set; }
        [BsonIgnore] //Bu annotasyon Category için extra bir kolon oluşturmayıp sadece CategoryId'yi almayı sağlayarak böylece eşleştirmeyi sağlar.
        public Category Category { get; set; }

        
    }

}
