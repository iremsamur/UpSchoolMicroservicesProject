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
                       }
                        ,
                        new ApiResource("Resources_Payment"){
                           Scopes={"Payment_FullPermission"}
                       },
                        new ApiResource("Resources_Photo_Stock"){
                           Scopes={ "Photo_Stock_FullPermission" }
                       },
                         //şimdi bu yukarıdaki apiresource'ları identity ile eşleştirelim.
                      new ApiResource(IdentityServerConstants.LocalApi.ScopeName)//eşleştirme için bunu ekliyoruz

                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                //tüm apileri alias takma isimleri ile burada yazıyoruz.
                //yeni bir api kaynağı tanımlayalım. Uygulamamızda identity dışında Catalog apisi var. Bu apilere alias veriyorum. İleride bunlara erişimi düzenlemek için

                //Tüm servisler için bu yazılır.
                new ApiScope("Catalog_FullPermission","Catalog API için tam yetkili erişim."),
                //mesela Catalog dışında başka bir servise daha izin tanımlayalım. Mesela Order mikroservisi için olsun.
                new ApiScope("Order_FullPermission","Sipariş API için tam yetkili erişim."),

                //Discount servisi için
                  new ApiScope("Discount_FullPermission","İndirim API için tam yetkili erişim."),

                  //Sepet için
                  new ApiScope("Basket_FullPermission","Sepet API için tam yetkili erişim."),

                  //ödeme için
                    new ApiScope("Payment_FullPermission","Ödeme API için tam yetkili erişim."),

                    //Fotoğraflar
                      new ApiScope("Photo_Stock_FullPermission","Fotoğraf API için tam yetkili erişim."),

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
                    ClientId = "mvcclient",
                    ClientName = "asp.netcoremvc",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    //tüm full permissionları burada yazılır. Yani eğer client geçerse buradaki tüm scopelara erişim izni olacak
                    AllowedScopes = { "Catalog_FullPermission", "Order_FullPermission" , "Discount_FullPermission", "Basket_FullPermission", "Payment_FullPermission", "Photo_Stock_FullPermission", IdentityServerConstants.LocalApi.ScopeName }
                    //Eğer benim tokenımın süresi bitmişse (mesela 1 saat) otantike olmamışsa yetkisi yoksa, token almamışsa bu alanlara erişemeycek gibi kontrolleri yapmam gerekiyor. 
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:44300/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "scope2" }
                },
            };



    }
}