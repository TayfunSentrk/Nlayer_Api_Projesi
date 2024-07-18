using Nlayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Core.Repositories
{
    /// <summary>
    /// ICategoryRepository arayüzü, kategori verilerini yönetmek için gerekli metodları tanımlar.
    /// GetSingleCategoryByIdWithProduct metodu, belirli bir kategori ve bu kategoriye ait ürünleri getirir.
    /// </summary>
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        /// <summary>
        /// GetSingleCategoryByIdWithProduct metodu, belirli bir kategori ve bu kategoriye ait ürünleri getirir.
        /// </summary>
        /// <param name="categoryId">Getirilecek kategorinin ID'si.</param>
        /// <returns>
        /// Belirtilen ID'ye sahip kategori ve bu kategoriye ait ürünlerin bulunduğu bir liste döner.
        /// Kategori, ilişkili ürünleri içeren tek bir Category nesnesi olarak döner.
        /// Örneğin:
        /// - Category
        ///     - ProductA
        ///     - ProductB
        /// </returns>
        public Task<Category> GetSingleCategoryByIdWithProduct(int categoryId);
    }

}
