using Nlayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Core.Repositories
{
    /// <summary>
    /// Ürünlerle ilgili özel repository arayüzü
    /// </summary>
    public interface IProductRepository : IGenericRepository<Product>
    {
        /// <summary>
        /// Kategorileriyle birlikte ürünleri getiren metot
        /// </summary>
        /// <returns>Kategorileriyle birlikte ürünlerin listesini döner</returns>
        Task<List<Product>> GetProductWithCategory();
    }
}
