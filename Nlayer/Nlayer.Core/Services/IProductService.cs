using Nlayer.Core.Dtos;
using Nlayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Core.Services
{
    /// <summary>
    /// Ürünlerle ilgili servis arayüzü
    /// </summary>
    public interface IProductService : IService<Product>
    {
        /// <summary>
        /// Kategorileriyle birlikte ürünleri asenkron olarak getiren metot
        /// </summary>
        /// <returns>Kategorileriyle birlikte ürünlerin listesini döner</returns>
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategoryAsync();
    }
}
