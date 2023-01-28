using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UpSchoolECommerce.Order.Domain.Core;

namespace UpSchoolECommerce.Order.Domain.OrderAggregate
{
    public class Order : Entity,IAggregateRoot
    {
      
        //Burada siparişin genel bilgileri alınır. Satın alan, tarih gibi
        public DateTime CreatedDate { get; set; }

        public Address Address { get; set; }//sipariş nereye teslim edilecek
        public string BuyerId { get; set; }

        
        private readonly List<OrderItem> _OrderItems;//buna diğerleri tarafından erişilmemesi gerekiyor.
        //siparişin ögeleri tutulacak

        public IReadOnlyCollection<OrderItem> OrderItems => _OrderItems;

        public Order()
        {

        }
        //Projenin ilk çalıştığı anda gerçekleştireceği işlemleri burada yazıyoruz
        public Order(string buyerId,Address address)
        {
            _OrderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            BuyerId = buyerId;
            Address = address;
        }

        //siparişi ekleme
        public void AddOrderItem(string productId,string productName,decimal price, string pictureURL)
        {
            var existProduct = _OrderItems.Any(x => x.ProductId == productId);//ürün var mı kontrol ediliyor
            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureURL, price);
                _OrderItems.Add(newOrderItem);
            }


            

        }
       
        //toplam fiyatı hesapla
        public decimal GetTotalPrice => _OrderItems.Sum(x => x.Price);//siparişin toplam tutarını verecek

    }
}
