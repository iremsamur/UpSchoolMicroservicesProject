// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace UpSchoolECommerce.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources =>
                   new ApiResource[]
                   {
         
                       //mikroservislerin hepsini tanımlıyorum
                       //aşağıda yazılmış tüm api scope'ların takma isimlerini burada yazıyorum.
                       new ApiResource("Resources_Catalog"){
                           Scopes={"Catalog_FullPermission"}
                       },
                       
                        new ApiResource("Resources_Order"){
                           Scopes={"Order_FullPermission"}
                       }
                        ,
                        new ApiResource("Resources_Discount"){
                           Scopes={"Discount_FullPermission"}
                       },
                       new ApiResource("Resources_Basket"){
                           Scopes={"Basket_FullPermission"}
                       },
                       // ,
                        new ApiResource("Resources_Payment"){
                           Scopes={"Payment_FullPermission"}
                       },
                        new ApiResource("Resources_Photo_Stock"){
                           Scopes={ "Photo_Stock_FullPermission" }
                       },
                        //gateways için api resource
                        new ApiResource("Resources_Gateway"){
                           Scopes={ "Gateway_FullPermission" }
                       },
                         //şimdi bu yukarıdaki apiresource'ları identity ile eşleştirelim.
                      new ApiResource(IdentityServerConstants.LocalApi.ScopeName)//eşleştirme için bunu ekliyoruz

                   };

        //IdentityResource yazalım
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                //yeni bir identity kaynağı oluşturuyoruz
                new IdentityResources.Email(),

                new IdentityResources.Profile(),
                new IdentityResources.OpenId()//id değerini alacak



            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                //tüm apileri alias takma isimleri ile burada yazıyoruz.
                //yeni bir api kaynağı tanımlayalım. Uygulamamızda identity dışında Catalog apisi var. Bu apilere alias veriyorum. İleride bunlara erişimi düzenlemek için

                //Tüm servisler için bu yazılır.
                new ApiScope("Catalog_FullPermission","Catalog API için tam yetkili erişim."),
                //mesela Catalog dışında başka bir servise daha izin tanımlayalım. Mesela Order mikroservisi için olsun.
                //new ApiScope("Order_FullPermission","Sipariş API için tam yetkili erişim."),

                ////Discount servisi için
                  new ApiScope("Discount_FullPermission","İndirim API için tam yetkili erişim."),

                //  //Sepet için
                  new ApiScope("Basket_FullPermission","Sepet API için tam yetkili erişim."),

                //  //ödeme için
                    new ApiScope("Payment_FullPermission","Ödeme API için tam yetkili erişim."),

                    //Fotoğraflar
                      new ApiScope("Photo_Stock_FullPermission","Fotoğraf API için tam yetkili erişim."),

                      //Orders
                       new ApiScope("Order_FullPermission","Sipariş API için tam yetkili erişim."),

                      //Gateways
                      new ApiScope("Gateway_FullPermission","Gateway API için tam yetkili erişim."),

                      //şimdi bu yukarıdaki apiscope'ları identity ile eşleştirelim.
                      new ApiScope(IdentityServerConstants.LocalApi.ScopeName)//eşleştirme için bunu ekliyoruz

            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                //apilere istek atacak tarafın bilgileri

                // m2m client credentials flow client
                new Client
                {
                    //Bu Client sisteme otantike olmadan, giriş yapmadan bunu çalıştırıp bir kişi sisteme kaydedilecek
                    ClientId = "mvcclient",
                    ClientName = "asp.netcoremvc",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,//kullanıcının nelere ulaşabileceği
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    //tüm full permissionları burada yazılır. Yani eğer client geçerse buradaki tüm scopelara erişim izni olacak
                    AllowedScopes = { "Catalog_FullPermission", /*"Order_FullPermission" , "Discount_FullPermission", "Basket_FullPermission", "Payment_FullPermission",*/ "Photo_Stock_FullPermission",
                        "Gateway_FullPermission",
                        IdentityServerConstants.LocalApi.ScopeName }
                    //giriş yapmadan gelen kullanıcı bu sayfalara erişim sağlayabilecek

                    //Eğer benim tokenımın süresi bitmişse (mesela 1 saat) otantike olmamışsa yetkisi yoksa, token almamışsa bu alanlara erişemeycek gibi kontrolleri yapmam gerekiyor. 
                },

                // interactive client using code flow + pkce
                new Client
                {
                    //Bu ise sisteme otantike olduktan sonra gelecek kullanıcı için olacak
                    ClientId = "mvcclientforuser",
                    ClientName="aspnetcore",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,//ResourceOwnerPassword ile yazdığımız IdentityResourceOwnerPasswordValidator'ın devreye girmesini sağlar.
                    AccessTokenLifetime=300,//token'ın geçerlilik süresini veriyoruz. Bu token 300 sn geçerli olacak
                    //300 sn sonra geçerliliğini yitirecek
                   

                    AllowOfflineAccess = true,
                    AllowedScopes = { "Catalog_FullPermission","Basket_FullPermission", "Gateway_FullPermission", "Payment_FullPermission", "Discount_FullPermission", "Order_FullPermission", IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OfflineAccess,IdentityServerConstants.LocalApi.ScopeName}//böylece token'ın içerisinde email de bizi karşılar. Böylece bu {} içinde belirttiklerimi token içerisinde gireceğiz.
                    //giriş yapan kullanıcının erişmesi gereken sayfalar buraya yazılır.
                    
                },
            };



    }
}