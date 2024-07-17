using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {

        //IUnitOfWork kullanmamın sebebi tek bir transaction bloğunda veritabanına yansıtma işlemi yapar
        //Herhangi bir hata olduğunda diğerlerini rollback yapar
        //Save || saveasync methodunu merkezi bi yerden yönetmeyi sağlar

        /// <summary>
        /// Entityframework saveChangeasync methodunu çağırır
        /// </summary>

        /// <returns>Asenkron işlemi temsil eden bir görev. Görev Sonucunda Entityframework saveChangeasync methodunu çağırır </returns>
        Task CommitAsync();

        /// <summary>
        /// Entityframework saveChange methodunu çağırır
        /// </summary>

        /// <returns> Görev Sonucunda Entityframework saveChange methodunu çağırır </returns>
        void Commit();
    }
}
