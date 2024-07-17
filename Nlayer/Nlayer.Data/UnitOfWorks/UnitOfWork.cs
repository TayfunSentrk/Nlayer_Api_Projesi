using Nlayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        /// <summary>
        /// Veritabanına kaydetmek için merkezi bir yerden asekron olmayan savechanges methodunu uygular
        /// </summary>
        
        /// <returns>Herhangi bir şeye geri dönmeyen method </returns>
        public void Commit()
        {
           _appDbContext.SaveChanges();
        }
        /// <summary>
        /// Veritabanına kaydetmek için merkezi bir yerden asekron olan savechangesAsync methodunu uygular
        /// </summary>

        /// <returns> Asekron bir işlem sonucunda Herhangi bir şeye geri dönmeyen method </returns>
        public async Task CommitAsync()
        {
           await _appDbContext.SaveChangesAsync();
        }
    }
}
