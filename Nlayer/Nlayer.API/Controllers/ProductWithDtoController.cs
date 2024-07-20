using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nlayer.API.Filters;
using Nlayer.Core.Dtos;
using Nlayer.Core.Models;
using Nlayer.Core.Services;

namespace Nlayer.API.Controllers
{

    public class ProductWithDtoController : CustomBaseController
    {
        private readonly IProductServiceWithDto productServiceWithDto;

        public ProductWithDtoController(IProductServiceWithDto productServiceWithDto)
        {
            this.productServiceWithDto = productServiceWithDto;
        }

        /// <summary>
        /// Tüm ürünleri getiren metot.
        /// </summary>
        /// <returns>Tüm ürünlerin listesini döner.</returns>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            return CreateActionResult(await productServiceWithDto.GetAll());
        }

        /// <summary>
        /// Belirli bir ID'ye sahip ürünü getiren metot.
        /// </summary>
        /// <param name="id">Ürünün ID'si.</param>
        /// <returns>Belirtilen ID'ye sahip ürün bilgilerini döner.</returns>
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await productServiceWithDto.GetByIdAsync(id));
        }

        /// <summary>
        /// Yeni bir ürün kaydeden metot.
        /// </summary>
        /// <param name="product">Kaydedilecek ürün DTO'su.</param>
        /// <returns>Kaydedilen ürün bilgilerini döner.</returns>
        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto product)
        {
            return CreateActionResult(await productServiceWithDto.AddAsync(product));
        }

        /// <summary>
        /// Mevcut bir ürünü güncelleyen metot.
        /// </summary>
        /// <param name="product">Güncellenecek ürün DTO'su.</param>
        /// <returns>Başarılı sonuç döner (içerik yok).</returns>
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto product)
        {
            return CreateActionResult(await productServiceWithDto.UpdateAsync(product));
        }

        /// <summary>
        /// Belirli bir ID'ye sahip ürünü silen metot.
        /// </summary>
        /// <param name="id">Silinecek ürünün ID'si.</param>
        /// <returns>Başarılı sonuç döner (içerik yok).</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await productServiceWithDto.RemoveAsync(id));
        }

        /// <summary>
        /// Ürünleri kategorileriyle birlikte getiren metot.
        /// </summary>
        /// <returns>Ürün ve kategorilerinin listesini döner.</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithCategory()
        {
            return CreateActionResult(await productServiceWithDto.GetProductsWithCategoryAsync());
        }
    }

}
