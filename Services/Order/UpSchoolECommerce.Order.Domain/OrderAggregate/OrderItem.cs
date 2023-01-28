using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchoolECommerce.Order.Domain.Core;

namespace UpSchoolECommerce.Order.Domain.OrderAggregate
{
    //Bu sınıfta siparişin tek tek ögeleri alınır.
    public class OrderItem : Entity
        //OrderItem Entity sınıfından miras alarak OrderItem
        //sınıfında bulunan parametrelerin validasyon kontrolleri yapılarak geçerliliği sağlanacak
        //Örneğin gönderdiğim parametre istediğim aralıkta mı değil mi gibi
    {
       
        //siparişteki ürünler
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string PictureURL { get; set; }
        public decimal Price { get; set; }

        public OrderItem()
        {

        }

        public OrderItem(string productId, string name, string pictureURL, decimal price)
        {
            ProductId = productId;
            Name = name;
            PictureURL = pictureURL;
            Price = price;
        }

        //sipariş güncellemesi
        public void UpdateOrderItem(string name, string pictureURL, decimal price)
        {
            Name = name;
            PictureURL = pictureURL;
            Price = price;
        }




    }
}
