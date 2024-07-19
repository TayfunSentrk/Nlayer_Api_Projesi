﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nlayer.Core.Dtos;
using Nlayer.Core.Models;
using Nlayer.Core.Services;

namespace Nlayer.Web.Controllers
{/// <summary>
 /// Ürün kaydetme işlemlerini yöneten kontrolcü.
 /// </summary>
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        /// <summary>
        /// <see cref="ProductsController"/> sınıfının yeni bir örneğini başlatır.
        /// </summary>
        /// <param name="productService">Ürün servis örneği.</param>
        /// <param name="categoryService">Kategori servis örneği.</param>
        /// <param name="mapper">Nesne dönüştürücü örneği.</param>
        public ProductsController(IProductService productService, ICategoryService categoryService, IMapper mapper)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Ürün ekleme sayfasını görüntüler.
        /// </summary>
        /// <returns>Ürün ekleme sayfasını döner.</returns>
        [HttpGet]
        public async Task<IActionResult> Save()
        {

            var categories = await categoryService.GetAll();
            var categoriesDto = mapper.Map<List<CategoryDto>>(categories.ToList());
           
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
                await productService.AddAsync(mapper.Map<Product>(productDto));
                return RedirectToAction(nameof(Index));
            }

            var categories = await categoryService.GetAll();
            var categoriesDto = mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            return View();
        }
    }

}
