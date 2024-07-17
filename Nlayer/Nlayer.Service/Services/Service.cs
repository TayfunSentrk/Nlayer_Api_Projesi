using Microsoft.EntityFrameworkCore;
using Nlayer.Core;
using Nlayer.Core.Repositories;
using Nlayer.Core.Services;
using Nlayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Service.Services
{
    public class Service<T> : IService<T> where T : BaseEntity, new()
    {
        private readonly IGenericRepository<T> genericRepository; //VERİTABANI İŞLEMLERİ İÇİN ıgeneric repository çağrıldı
        private readonly IUnitOfWork unitOfWork; // tek bir transaction'dan yönetmek için Iunitofwork çağırıldı

        public Service(IGenericRepository<T> genericRepository, IUnitOfWork unitOfWork)
        {
            this.genericRepository = genericRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Benzersiz kimliğiyle bir entity alır.
        /// </summary>
        /// <param name="entity">Alınmak istenen BaseEntity tipindeki entity' değeri.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda T tipinde entity döner,.</returns>
        public async Task<T> AddAsync(T entity)
        {
            await  genericRepository.AddAsync(entity);
            await unitOfWork.CommitAsync();
            return entity;
        }

        /// <summary>
        /// IEnumerable tipinde bir entities listesi alır.IEnumerable<T> tipinde T entites döner
        /// </summary>
        /// <param name="entities">Alınmak istenen BaseEntity tipindeki entities' listesi .</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda IEnumerable T tipinde entities döner,.</returns>
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await genericRepository.AddRangeAsync(entities); 
            await unitOfWork.CommitAsync();
            return entities;
        }

        /// <summary>
        ///expression ifadesi alır.Eğer doğruysa true değilse false döner
        /// </summary>
        /// <param name="expression">expression şart ifades.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda true veya false döner,.</returns>
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
           return await genericRepository.AnyAsync(expression);
        }

        /// <summary>
        /// <IEnumerable<T> listesi döner
        /// </summary>

        /// <returns>Asenkron işlemi temsil eden bir görev. IEnumerable tipinde T listesi döner,.</returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            return await genericRepository.GetAll().ToListAsync();
        }


        /// <summary>
        /// Benzersiz kimliğiyle bir id alır.
        /// </summary>
        /// <param name="id">Alınmak istenen BaseEntity tipindeki entity' nin primary keyi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda T tipinde entity döner,.</returns>
        public async Task<T> GetByIdAsync(int id)
        {
            return await genericRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Base entity tipinde bir entity alır.
        /// </summary>
        /// <param name="entity">Alınmak istenen BaseEntity tipindeki entity.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda T tipinde entity silme işlemi  gerçekleşir,.</returns>
        public async Task RemoveAsync(T entity)
        {
          genericRepository.Remove(entity);
           await unitOfWork.CommitAsync();
       
        }


        /// <summary>
        /// Base entity tipinde List ieklinde  bir entites alır.
        /// </summary>
        /// <param name="entites">Alınmak istenen BaseEntity tipindeki liste tipinde entity.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda IEnumerable list  T tipinde entities silme işlemi  gerçekleşir,.</returns>
        public async Task RemoveRangeAsync(IEnumerable<T> entites)
        {
           genericRepository.RemoveRange(entites);
           await unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Base entity tipinde entity alır.
        /// </summary>
        /// <param name="entity"> BaseEntity tipindeki  entity.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda T tipinde entity güncelleme işlemi yapar,.</returns>

        public async Task UpdateAsync(T entity)
        {
            genericRepository.Update(entity);
            await unitOfWork.CommitAsync();
        }

        /// <summary>
        ///Bir koşul ifadesi alır .bu koşula uyan değerleri  IQueryable<T> tipi şeklinde döner
        /// </summary>
        /// <param name="expression"> Bir koşul ifadesi alır.</param>
        /// <returns>Görev sonucunda IQueryable T tipinde liste döner,.</returns>
        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return genericRepository.Where(expression);
        }
    }
}
