using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UpSchoolECommerce.Services.Catalog.Models
{
    public class Category
    {
        //Mongodb'de alanlarda kullanmam gereken bazı attribute'lar var.
        //id'nin id olduğunu belirtmem için attribute kullanmam gerekiyor
        [BsonId]//alanın id olduğunu  belirtmek için kullanılır.
        [BsonRepresentation(BsonType.ObjectId)]//id'nin unique olduğunu belirtiyor.
        public string Id{ get; set; }
        public string Name { get; set; }

    }
}
