using Nlayer.Core.Dtos;
using Nlayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Core.Services
{
    public interface IProductServiceWithDto:IServiceWithDto<Product, ProductDto>
    {
        /// <summary>
        /// Kategorileriyle birlikte ürünleri asenkron olarak getiren metot
        /// </summary>
        /// <returns>Kategorileriyle birlikte ürünlerin listesini döner</returns>
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategoryAsync();

        /// <summary>
        /// entity güncelleme yarar.burada productupdate olarak parametre gelir
        /// </summary>
        /// <returns>Herhangi bir yapı dönmez</returns>
        /// burda override yaptık çünkü alış tipimiz productUpdatedDto

        Task<CustomResponseDto<NoContentDto>>UpdateAsync(ProductUpdateDto productUpdateDto);//burda 

        Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto productCreate);
    }
}
