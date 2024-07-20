using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    /// Ürün hizmeti sınıfı. Bu sınıf, ürünlerle ilgili veri transfer nesneleri (DTO) ile çalışır.
    /// </summary>
    public class ProductServiceWithDto : ServiceWithDto<Product, ProductDto>, IProductServiceWithDto
    {
        private readonly IProductRepository productRepository;

        /// <summary>
        /// <see cref="ProductServiceWithDto"/> sınıfının yeni bir örneğini başlatır.
        /// </summary>
        /// <param name="genericRepository">Genel amaçlı depo.</param>
        /// <param name="unitOfWork">Birimi iş.</param>
        /// <param name="mapper">Nesne eşleyici.</param>
        /// <param name="productRepository">Ürün deposu.</param>
        public ProductServiceWithDto(IGenericRepository<Product> genericRepository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(genericRepository, unitOfWork, mapper)
        {
            this.productRepository = productRepository;
        }

        /// <summary>
        /// Yeni bir ürünü asenkron olarak ekler.
        /// </summary>
        /// <param name="productCreate">Eklenecek ürün oluşturma DTO'su.</param>
        /// <returns>Eklenen ürün DTO'sunu içeren bir <see cref="CustomResponseDto{ProductDto}"/>.</returns>
        public async Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto productCreate)
        {
            var entity = mapper.Map<Product>(productCreate);
            await productRepository.AddAsync(entity);
            await unitOfWork.CommitAsync();
            var dto = mapper.Map<ProductDto>(productCreate);
            return CustomResponseDto<ProductDto>.Success(StatusCodes.Status201Created, dto);
        }

        /// <summary>
        /// Kategorileri ile birlikte ürünleri asenkron olarak getirir.
        /// </summary>
        /// <returns>Kategorileri ile birlikte ürünleri içeren bir <see cref="CustomResponseDto{List{ProductWithCategoryDto}}"/>.</returns>
        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategoryAsync()
        {
            var listProductWithCategoryDto = mapper.Map<List<ProductWithCategoryDto>>(await productRepository.GetProductWithCategory());
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(StatusCodes.Status200OK, listProductWithCategoryDto);
        }

        /// <summary>
        /// Mevcut bir ürünü asenkron olarak günceller.
        /// </summary>
        /// <param name="productUpdateDto">Güncellenecek ürün DTO'su.</param>
        /// <returns>Güncelleme işleminin sonucunu içeren bir <see cref="CustomResponseDto{NoContentDto}"/>.</returns>
        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var product = mapper.Map<Product>(productUpdateDto);
            productRepository.Update(product);
            await unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
    }

}
