using System;
using System.Collections.Generic;
using System.Text;

namespace UpSchoolECommerce.Shared.Services
{
    public interface ISharedIdentityService
    {
        //token içindeki sub'dan kullanıcı id'sini alacak. Crud işlemlerini bu id'ye göre yapabilmesi için gerekli
        public string GetUserId { get;  }
    }
}
