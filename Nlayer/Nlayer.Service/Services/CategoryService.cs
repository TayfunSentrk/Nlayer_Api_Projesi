using AutoMapper;
using Nlayer.Core.Dtos;
using Nlayer.Core.Models;
using Nlayer.Core.Repositories;
using Nlayer.Core.Services;
using Nlayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Service.Services
{
    /// <summary>
    /// CategoryService sınıfı, kategori verilerini yönetmek için gerekli servis metodlarını uygular.
    /// Service sınıfından türetilmiştir ve ICategoryService arayüzünü uygular.
    /// </summary>
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly IMapper mapper;
        private readonly ICategoryRepository categoryRepository;

        /// <summary>
        /// CategoryService yapıcı metodu, gerekli bağımlılıkları alır ve üst sınıf olan Service'e iletir.
        /// </summary>
        /// <param name="genericRepository">Generic repository</param>
        /// <param name="unitOfWork">Unit of Work</param>
        /// <param name="mapper">Nesneleri eşlemek için kullanılan IMapper</param>
        /// <param name="categoryRepository">Kategori repository</param>
        public CategoryService(IGenericRepository<Category> genericRepository, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository)
            : base(genericRepository, unitOfWork)
        {
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
        }

        /// <summary>
        /// GetSingleCategoryByIdWithProductAsync metodu, belirli bir kategori ve bu kategoriye ait ürünleri getirir.
        /// </summary>
        /// <param name="categoryId">Getirilecek kategorinin ID'si</param>
        /// <returns>
        /// Belirtilen ID'ye sahip kategori ve bu kategoriye ait ürünlerin bulunduğu CategoryWithProducts DTO'sunu içeren bir CustomResponseDto döner.
        /// Örneğin:
        /// - CategoryWithProducts
        ///     - CategoryId
        ///     - CategoryName
        ///     - Products
        ///         - ProductA
        ///         - ProductB
        /// </returns>
        public async Task<CustomResponseDto<CategoryWithProducts>> GetSingleCategoryByIdWithProductAsync(int categoryId)
        {
            // Veritabanından belirtilen kategori ID'sine sahip kategoriyi ve bu kategoriye ait ürünleri getirir, 
            // daha sonra bu verileri CategoryWithProducts DTO'suna map eder ve başarı durumu ile birlikte döner.
            var categoryWithProducts = await categoryRepository.GetSingleCategoryByIdWithProduct(categoryId);
            var categoryWithProductsDto = mapper.Map<CategoryWithProducts>(categoryWithProducts);
            return CustomResponseDto<CategoryWithProducts>.Success(200, categoryWithProductsDto);
        }
    }
}
