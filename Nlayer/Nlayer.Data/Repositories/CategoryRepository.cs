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
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    namespace Nlayer.Core.Repositories
    {
        /// <summary>
        /// CategoryRepository sınıfı, kategori verilerini yönetmek için gerekli metodları uygular.
        /// GenericRepository'den türetilmiştir ve ICategoryRepository arayüzünü uygular.
        /// </summary>
        public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
        {
            /// <summary>
            /// CategoryRepository yapıcı metodu, veritabanı bağlamını alır ve üst sınıf olan GenericRepository'e iletir.
            /// </summary>
            /// <param name="dbContext">Veritabanı bağlamı</param>
            public CategoryRepository(AppDbContext dbContext) : base(dbContext)
            {
            }

            /// <summary>
            /// GetSingleCategoryByIdWithProduct metodu, belirli bir kategori ve bu kategoriye ait ürünleri getirir.
            /// </summary>
            /// <param name="categoryId">Getirilecek kategorinin ID'si</param>
            /// <returns>
            /// Belirtilen ID'ye sahip kategori ve bu kategoriye ait ürünlerin bulunduğu bir liste döner.
            /// Kategori, ilişkili ürünleri içeren tek bir Category nesnesi olarak döner.
            /// Örneğin:
            /// - Category
            ///     - ProductA
            ///     - ProductB
            /// </returns>
            public async Task<Category> GetSingleCategoryByIdWithProduct(int categoryId)
            {
                // Veritabanından belirtilen kategori ID'sine sahip kategoriyi ve bu kategoriye ait ürünleri getirir.
                return await _dbContext.Categories.Include(c => c.Products).Where(c => c.Id == categoryId).SingleOrDefaultAsync();
                  
                  
            }
        }
    }

}
