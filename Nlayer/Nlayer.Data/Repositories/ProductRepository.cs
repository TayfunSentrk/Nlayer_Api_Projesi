using Microsoft.EntityFrameworkCore;
using Nlayer.Core.Models;
using Nlayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Data.Repositories
{
    /// <summary>
    /// Ürün repository'sinin somut sınıfı
    /// </summary>
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        /// <summary>
        /// Ürün repository sınıfının kurucusu, AppDbContext bağımlılığını alır
        /// </summary>
        /// <param name="dbContext">Veritabanı bağlamı</param>
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Kategorileriyle birlikte ürünleri getiren metot
        /// </summary>
        /// <returns>Kategorileriyle birlikte ürünlerin listesini döner</returns>
        public async Task<List<Product>> GetProductWithCategory()
        {
            // Ürünleri kategorileriyle birlikte veritabanından alır ve liste olarak döner
            return await _dbContext.Products.Include(p => p.Category).ToListAsync();
        }
    }
}
