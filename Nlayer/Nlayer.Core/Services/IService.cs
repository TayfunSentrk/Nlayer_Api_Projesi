using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Core.Services
{
    public interface IService<T> where T : BaseEntity, new()
    {
        /// <summary>
        /// Benzersiz kimliğiyle bir Id alır.
        /// </summary>
        /// <param name="id">Alınmak istenen BaseEntity tipindeki entity'nin benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda Base entity  tiinde bir entity içerir,.</returns>
        Task<T> GetByIdAsync(int id);


        // <summary>
        ///Veritabanı gitmeden sorgular yapmamızı sağlar .Asekron değil çünkü veritabanı sorgusu değil.veritabanı yapıcak sorgu öncesi işlemler
        /// <param name="expression">Function Delegesi. T tipinde entity alır true veya false değeri alır </param>
        /// </summary>

        Task<IEnumerable<T>> GetAll();
        // <summary>
        ///Tüm datayı almamızı sağlar
        /// 
        /// </summary>

        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda IEnumerable tipinde T entites döner,</returns>
        IQueryable<T> Where(Expression<Func<T, bool>> expression);


        /// <summary>
        /// Benzersiz kimliğiyle bir entity alır.
        /// </summary>
        /// <param name="entity">Alınmak istenen BaseEntity tipindeki entity.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda veritabına T tipinde entity ekler.</returns>
        Task AddAsync(T entity);


        /// <summary>
        /// IEnumerable listesi şeklinde T Tipinde entityler alır
        /// </summary>
        /// <param name="entities">Alınmak istenen BaseEntity tipindeki entity tipinde liste.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda veritabına T tipinde list  entities ekler.</returns>
        Task AddRangeAsync(IEnumerable<T> entities);

        // <summary>
        /// Belirli bir koşula göre true veya false değeri döner
        /// <param name="expression">Function Delegesi. T tipinde entity alır true veya false değeri alır </param>
        /// </summary>
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);


        /// <summary>
        ///  T Tipinde entity alır
        /// </summary>
        /// <param name="entity">Alınmak istenen BaseEntity tipindeki entity.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda veritabına T tipinde entity update işlemi gerçekleşitir </returns>
        Task Update(T entity);


        /// <summary>
        ///  T Tipinde entity alır
        /// </summary>
        /// <param name="entity">Alınmak istenen BaseEntity tipindeki entity.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda veritabına T tipinde entity silme işlemi gerçekleşir </returns>
        Task Remove(T entity);

        /// <summary>
        ///  IEnumerable T Tipinde liste alır
        /// </summary>
        /// <param name="entites">Alınmak istenen BaseEntity tipindeki entity listesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda veritabına T tipinde list entitileri silme işlemi gerçekleşir .</returns>
        Task RemoveRange(IEnumerable<T> entites);
    }
}
