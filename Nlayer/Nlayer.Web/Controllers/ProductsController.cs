using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nlayer.Core.Dtos;
using Nlayer.Core.Models;
using Nlayer.Core.Services;
using Nlayer.Web.Services;

namespace Nlayer.Web.Controllers
{/// <summary>
 /// Ürün kaydetme işlemlerini yöneten kontrolcü.
 /// </summary>

 /// <summary>
 /// Ürün kaydetme işlemlerini yöneten kontrolcü.
 /// </summary>
    public class ProductsController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Ürün kaydetme işlemlerini yöneten kontrolcü.
        /// </summary>
        /// <param name="productApiService">Ürün servisi.</param>
        /// <param name="categoryApiService">Kategori servisi.</param>
        /// <param name="mapper">Nesne eşleyici.</param>
        public ProductsController(ProductApiService productApiService, CategoryApiService categoryApiService, IMapper mapper)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
            _mapper = mapper;
        }

        /// <summary>
        /// Ürünlerin ve kategorilerinin listesini görüntüler.
        /// </summary>
        /// <returns>Ürünlerin ve kategorilerinin listesi ile görünümü döner.</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _productApiService.GetProductWithCategoryDtosAsync());
        }

        /// <summary>
        /// Ürün ekleme sayfasını görüntüler.
        /// </summary>
        /// <returns>Ürün ekleme sayfasını döner.</returns>
        [HttpGet]
        public async Task<IActionResult> Save()
        {
            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }

        /// <summary>
        /// Yeni bir ürünü kaydeder.
        /// </summary>
        /// <param name="productDto">Kaydedilecek ürün bilgileri.</param>
        /// <returns>İşlem başarılıysa ürün listesini döner, değilse ürün ekleme sayfasını döner.</returns>
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.SaveAsync(productDto);
                return RedirectToAction(nameof(Index));
            }

            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }

        /// <summary>
        /// Ürün güncelleme sayfasını görüntüler.
        /// </summary>
        /// <param name="id">Güncellenecek ürünün kimliği.</param>
        /// <returns>Ürün güncelleme sayfasını döner.</returns>
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            var productUpdatedDto = _mapper.Map<ProductUpdateDto>(await _productApiService.GetByIdAsync(id));
            return View(productUpdatedDto);
        }

        /// <summary>
        /// Ürünü günceller.
        /// </summary>
        /// <param name="productUpdateDto">Güncellenecek ürün bilgileri.</param>
        /// <returns>İşlem başarılıysa ürün listesini döner, değilse ürün güncelleme sayfasını döner.</returns>
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.UpdateAsync(productUpdateDto);
                return RedirectToAction(nameof(Index));
            }

            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View(productUpdateDto);
        }

        /// <summary>
        /// Ürünü siler.
        /// </summary>
        /// <param name="id">Silinecek ürünün kimliği.</param>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _productApiService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }


}
