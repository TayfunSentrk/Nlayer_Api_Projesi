using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nlayer.Core.Dtos;
using Nlayer.Core.Models;
using Nlayer.Core.Services;

namespace Nlayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// Ürünlerle ilgili işlemleri gerçekleştiren kontrolcü sınıfı
    /// </summary>
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper mapper;
        private readonly IService<Product> service;

        /// <summary>
        /// IMapper ve IService<Product> bağımlılıklarını enjekte eden kurucu metot
        /// </summary>
        /// <param name="mapper">IMapper nesnesi</param>
        /// <param name="service">IService<Product> nesnesi</param>
        public ProductsController(IMapper mapper, IService<Product> service)
        {
            this.mapper = mapper;
            this.service = service;
        }

        /// <summary>
        /// Tüm ürünleri getiren metot
        /// </summary>
        /// <returns>Ürünlerin listesini döner</returns>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            // Tüm ürünleri al
            var products = await service.GetAll();
            // Ürünleri DTO'ya dönüştür
            var productsDto = mapper.Map<List<ProductDto>>(products.ToList());

            // Başarılı sonuç döner
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
        }

        /// <summary>
        /// Belirli bir ID'ye sahip ürünü getiren metot
        /// </summary>
        /// <param name="id">Ürünün ID'si</param>
        /// <returns>Ürün bilgilerini döner</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // ID'ye göre ürünü al
            var product = await service.GetByIdAsync(id);
            // Ürünü DTO'ya dönüştür
            var productDto = mapper.Map<ProductDto>(product);

            // Başarılı sonuç döner
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        /// <summary>
        /// Yeni bir ürün kaydeden metot
        /// </summary>
        /// <param name="product">Kaydedilecek ürün DTO'su</param>
        /// <returns>Kaydedilen ürün bilgilerini döner</returns>
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto product)
        {
            // Yeni ürünü ekle
            var createdProduct = await service.AddAsync(mapper.Map<Product>(product));
            // Eklenen ürünü DTO'ya dönüştür
            var productDto = mapper.Map<ProductDto>(createdProduct);

            // Başarılı sonuç döner
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productDto));
        }

        /// <summary>
        /// Mevcut bir ürünü güncelleyen metot
        /// </summary>
        /// <param name="product">Güncellenecek ürün DTO'su</param>
        /// <returns>Başarılı sonuç döner (içerik yok)</returns>
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto product)
        {
            // Ürünü güncelle
            await service.UpdateAsync(mapper.Map<Product>(product));

            // Başarılı sonuç döner (içerik yok)
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        /// <summary>
        /// Belirli bir ID'ye sahip ürünü silen metot
        /// </summary>
        /// <param name="id">Silinecek ürünün ID'si</param>
        /// <returns>Başarılı sonuç döner (içerik yok)</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // ID'ye göre ürünü al
            var product = await service.GetByIdAsync(id);
            // Ürünü sil
            await service.RemoveAsync(product);

            // Başarılı sonuç döner (içerik yok)
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }

}
