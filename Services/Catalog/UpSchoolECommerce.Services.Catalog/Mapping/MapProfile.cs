using AutoMapper;
using UpSchoolECommerce.Services.Catalog.Dtos;
using UpSchoolECommerce.Services.Catalog.Models;

namespace UpSchoolECommerce.Services.Catalog.Mapping
{
    public class MapProfile : Profile //Automapper kütüphanesinden geliyor
    {
        public MapProfile()
        {
            //tüm dto ve entity'ler için yazıyoruz
            //1.yol
            //mapping işlemini yazalım.
            CreateMap<Category, CategoryDto>();//Category sınıfı ile CategoryDto maplenirç
            CreateMap<CategoryDto, Category>();//Buda yukarıdakinin tam tersi
                                               //CategoryDto ile Category maplenir.

            //2.yol
            //Kategori içinde yazdığımız gibi ayrı ayrı kendisi ve tersi için map oluşturmak yerine Category,CategoryDto için map yazıp,
            //sonuna ReverseMap() metodu eklersem tersini de yapmasını otomatik sağlar
            CreateMap<Product, ProductDto>().ReverseMap();

            //CreateMap<ProductDto, Product>();bunu yazmadan bunuda yapmasını sağlar

            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();


        }

    }
}
