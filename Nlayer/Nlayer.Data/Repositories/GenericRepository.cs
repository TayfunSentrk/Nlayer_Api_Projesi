using Microsoft.EntityFrameworkCore;
using Nlayer.Core.Models;
using Nlayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {

        protected readonly AppDbContext _dbContext; // protected kullanmamın sebebi Başka ayrı methodlar lazım olursa product ile ilgili category'de almak isteyebilirim
        private readonly DbSet<T> _dbSet; //veritabanındaki tablo karşılığı readonly kullanmanın sebebi sadece contructor'da değer atamak için.Başka yerde hata almak için

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet=dbContext.Set<T>();
        }

        /// <summary>
        /// Benzersiz kimliğiyle bir entity alır.
        /// </summary>
        /// <param name="entity">Alınmak istenen BaseEntity tipindeki entity' değeri.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda entity ekleme işlemi yapar,.</returns>
        public async Task AddAsync(T entity)
        {
           await _dbSet.AddAsync(entity);
        }


        /// <summary>
        /// Benzersiz kimliğiyle bir entities alır.
        /// </summary>
        /// <param name="entities">Alınmak istenen BaseEntity tipindeki entity' listesi alır .</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda entity listesi ekleme işlemi yapar,.</returns>
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }


        /// <summary>
        /// Bir expression koşul ifadesi alır
        /// </summary>
        /// <param name="expression">Bir koşul ifadesi alır .</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda true veya false döner.</returns>
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
           return await _dbSet.AnyAsync(expression);
        }


        /// <summary>
        /// Bir expression koşul ifadesi alır an notracking memory almasın performansı düşürmesin diye
        /// </summary>
        /// <param name="expression">Bir koşul ifadesi alır .</param>
        /// <returns>Görev sonucunda IQueryable list tipinde T entites döner</returns>
        public IQueryable<T> GetAll()
        {
           return _dbSet.AsNoTracking().AsQueryable();
        }

        /// <summary>
        /// Benzersiz bir id alır T tipinde entity döner
        /// </summary>
        /// <param name="id">bir id alır .</param>
        /// <returns>Asenkron işlemi temsil eden bir görev Görev sonucunda T tipinde entity döner </returns>
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Bir entity alır Veri tabanından silmek için state değiştirilir
        /// </summary>
        /// <param name="entity">Base entity tipinde entity alır .</param>
        /// <returns>Herhangi bir değer dönmeyen bir method </returns>
        public void Remove(T entity)
        {
           _dbSet.Remove(entity);
        }

        /// <summary>
        /// Bir entites alır Veri tabanından  silmek için state'ler değiştirilir
        /// </summary>
        /// <param name="entites">Base entity tipinde entities listesi alır .</param>
        /// <returns>Herhangi bir değer dönmeyen bir method </returns>

        public void RemoveRange(IEnumerable<T> entites)
        {
            _dbSet.RemoveRange(entites);
        }

        /// <summary>
        /// Bir entity alır Veri tabanında güncellemek için state değiştirilir
        /// </summary>
        /// <param name="entity">Base entity tipinde entity alır .</param>
        /// <returns>Herhangi bir değer dönmeyen bir method </returns>
        public void Update(T entity)
        {
           _dbSet.Update(entity);
        }

        /// <summary>
        /// Bir expression ifadesi alır IQueryable tipinde T entites listedi döner
        /// </summary>
        /// <param name="expression"> Bir expression ifadesi.</param>
        /// <returns>IQueryable tipinde T entites listedi döner </returns>
        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.AsNoTracking().Where(expression); 
        }
    }
}
