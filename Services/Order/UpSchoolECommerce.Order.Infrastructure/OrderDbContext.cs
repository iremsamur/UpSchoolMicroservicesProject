using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSchoolECommerce.Order.Infrastructure
{
    public class OrderDbContext : DbContext
    {
        public const string Default_Schema = "ordering";
        public OrderDbContext(DbContextOptions<OrderDbContext> options):base(options)
        {
            //bu kullanımın anlamı bağlantı adresinin başka yerden verileceği.
            //Yani veritabanı connectionstring burada yazılmayacak
        }

        public DbSet<Domain.OrderAggregate.Order> Orders{ get; set; }

        public DbSet<Domain.OrderAggregate.OrderItem> OrderItems{ get; set; }

        //veritabanı tablolarla ilgili kısıtlamalar yapabileceğim, tablo isimlerini verebileceğim veya şema oluşturabileceğim yani çeşitli veritabanı değişiklikleri yapabileceğim bir metot yazalım
        //Bu kullanıma Fluent Api deniliyor
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //önce model içinde oluşturacağım tabloları burada belirleyeyim.
            //Yani bu yöntem migration'daki gibi değişiklikleri veritabanına yansıtma yöntemi
            modelBuilder.Entity<Domain.OrderAggregate.Order>().ToTable("Orders", Default_Schema);//Burada
            //Domain içindeki Order Entity'si ile ToTable içinde verdiğimiz Orders ismiyle ve yukarıda tanımladığımız şema ismi altında tabloyu oluşturmasını sağlıyor.
            //yani migration yerine burada tanımlamaları yapmış olduk. Bu da farklı bir yöntem
            //OrderItem tablo tanımlaması
            modelBuilder.Entity<Domain.OrderAggregate.OrderItem>().ToTable("OrderItems", Default_Schema);

            //Şimdi tabloya ait olacak kolonlara ait kısıtlamaları yazalım.
            modelBuilder.Entity<Domain.OrderAggregate.OrderItem>().Property(x=>x.Price).HasColumnType("decimal(18,2)");//Burada HasColumnType fonksiyonu ile Property içinde belirttiğimiz kolona sql'de yansıyacağı veri tipini belirtiyoruz.
            //decimal(18,2) ile decimal'in formatını belirliyoruz
            //Başka kısıtlamalarda yazalım.

            modelBuilder.Entity<Domain.OrderAggregate.OrderItem>().Property(x => x.Name).HasColumnType("nvarchar(100)");


            //Buradaki bu atamaları da ayrı bir class üzerinde tutup SOLID'e daha uygun bir kullanım olarak da yazılabilirdi

            modelBuilder.Entity<Domain.OrderAggregate.Order>().OwnsOne(o => o.Address).WithOwner();//Bunun anlamı OwnsOne ile benim Order sınıfımın
                                                                                                   //alt property'si Address olacak ama Order ile Address birbiriyle ilişkili olmayacak anlamına geliyor
            //Yani ilişki kurmadan onun içinde kullanmayı sağlıyor

            //Order ile OrderItem zaten ilişkili bunu Order içinde 20.satırda private readonly List<OrderItem> _OrderItems; ile zaten ilişkiyi belirttiğim için burada tekrar belirtmiyorum

            base.OnModelCreating(modelBuilder);
        }

    }
}
