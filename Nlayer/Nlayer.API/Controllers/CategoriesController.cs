using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nlayer.API.Filters;
using Nlayer.Core.Dtos;
using Nlayer.Core.Services;

namespace Nlayer.API.Controllers
{

    /// <summary>
    /// CategoriesController sınıfı, kategori verilerini yönetmek için gerekli API endpoint'lerini tanımlar.
    /// CustomBaseController sınıfından türetilmiştir.
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper mapper;
        /// <summary>
        /// CategoriesController yapıcı metodu, kategori servisini alır.
        /// </summary>
        /// <param name="categoryService">Kategori servisi</param>
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();   

            var categoriesDto=mapper.Map<List<CategoryDto>>(categories.ToList());
            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(200,categoriesDto));
        }

        /// <summary>
        /// Belirli bir kategori ve bu kategoriye ait ürünleri getirir.
        /// </summary>
        /// <param name="id">Getirilecek kategorinin ID'si</param>
        /// <returns>
        /// Belirtilen ID'ye sahip kategori ve bu kategoriye ait ürünlerin bulunduğu CategoryWithProducts DTO'sunu içeren bir IActionResult döner.
        /// Örneğin:
        /// - CategoryWithProducts
        ///     - CategoryId
        ///     - CategoryName
        ///     - Products
        ///         - ProductA
        ///         - ProductB
        /// </returns>
        /// 


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetCategoryByIdWithProducts(int id)
        {
            // Kategori ID'sine sahip kategoriyi ve bu kategoriye ait ürünleri getirir,
            // daha sonra sonucu CustomResponseDto formatında döner.
            var result = await _categoryService.GetSingleCategoryByIdWithProductAsync(id);
            return CreateActionResult(result);
        }
    }
}
