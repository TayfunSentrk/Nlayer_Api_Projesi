using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Nlayer.Core.Dtos;
using Nlayer.Core.Models;
using Nlayer.Core.Repositories;
using Nlayer.Core.Services;
using Nlayer.Core.UnitOfWorks;
using Nlayer.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Caching
{
    public class ProductServiceWithCaching : IProductService
    {
        // Sabitler
        private const string CacheProductKey = "productsCache";

        // Bağımlılıklar
        private readonly IMapper mapper; // Nesne dönüşümleri için
        private readonly IMemoryCache memoryCache; // Cacheleme için
        private readonly IProductRepository productRepository; // Veritabanı işlemleri için
        private readonly IUnitOfWork unitOfWork; // Veritabanı işlemlerini commit etmek için

        /// <summary>
        /// Bağımlılıkları başlatır ve ürünleri cache'te yoksa cacheler.
        /// </summary>
        /// <param name="mapper">Nesne dönüşümleri için IMapper.</param>
        /// <param name="memoryCache">Cacheleme için IMemoryCache.</param>
        /// <param name="productRepository">Veritabanı işlemleri için IProductRepository.</param>
        /// <param name="unitOfWork">Veritabanı işlemlerini commit etmek için IUnitOfWork.</param>
        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.memoryCache = memoryCache;
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;

            if (!memoryCache.TryGetValue(CacheProductKey, out _))
            {
                memoryCache.Set(CacheProductKey, productRepository.GetProductWithCategory()); // Cache'te yoksa ürünleri cachele
            }
        }

        /// <summary>
        /// Yeni bir ürün ekler ve cache'i günceller.
        /// </summary>
        /// <param name="entity">Eklenecek ürün.</param>
        /// <returns>Eklenen ürün.</returns>
        public async Task<Product> AddAsync(Product entity)
        {
            await productRepository.AddAsync(entity);
            await unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entity;
        }

        /// <summary>
        /// Birden fazla yeni ürünü ekler ve cache'i günceller.
        /// </summary>
        /// <param name="entities">Eklenecek ürünler koleksiyonu.</param>
        /// <returns>Eklenen ürünler koleksiyonu.</returns>
        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await productRepository.AddRangeAsync(entities);
            await unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entities;
        }

        /// <summary>
        /// Belirtilen şartlara göre bir ürünün var olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="expression">Kontrol edilecek şart.</param>
        /// <returns>Ürün varsa true, yoksa false.</returns>
        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            return Task.FromResult(memoryCache.Get<List<Product>>(CacheProductKey).Any(expression.Compile()));
        }

        /// <summary>
        /// Tüm ürünleri döner.
        /// </summary>
        /// <returns>Ürünler koleksiyonu.</returns>
        public Task<IEnumerable<Product>> GetAll()
        {
            return Task.FromResult(memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
        }

        /// <summary>
        /// ID'ye göre bir ürünü döner, bulunamazsa hata fırlatır.
        /// </summary>
        /// <param name="id">Ürün ID'si.</param>
        /// <returns>Belirtilen ID'ye sahip ürün.</returns>
        public Task<Product> GetByIdAsync(int id)
        {
            var product = memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new NotFoundException($"{typeof(Product).Name} bulunamadı");
            }
            return Task.FromResult(product);
        }

        /// <summary>
        /// Kategorilerle birlikte ürünleri döner.
        /// </summary>
        /// <returns>Kategorilerle birlikte ürünlerin CustomResponseDto'su.</returns>
        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategoryAsync()
        {
            return Task.FromResult(CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, mapper.Map<List<ProductWithCategoryDto>>(memoryCache.Get<List<Product>>(CacheProductKey))));
        }

        /// <summary>
        /// Bir ürünü siler ve cache'i günceller.
        /// </summary>
        /// <param name="entity">Silinecek ürün.</param>
        public async Task RemoveAsync(Product entity)
        {
            productRepository.Remove(entity);
            await unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        /// <summary>
        /// Birden fazla ürünü siler ve cache'i günceller.
        /// </summary>
        /// <param name="entities">Silinecek ürünler koleksiyonu.</param>
        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            productRepository.RemoveRange(entities);
            await unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        /// <summary>
        /// Bir ürünü günceller ve cache'i günceller.
        /// </summary>
        /// <param name="entity">Güncellenecek ürün.</param>
        public async Task UpdateAsync(Product entity)
        {
            productRepository.Update(entity);
            await unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        /// <summary>
        /// Belirtilen şartlara göre ürünleri döner.
        /// </summary>
        /// <param name="expression">Şart.</param>
        /// <returns>Şarta uyan ürünlerin sorgulanabilir koleksiyonu.</returns>
        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
        }

        /// <summary>
        /// Tüm ürünleri cache'e ekler.
        /// </summary>
        public async Task CacheAllProductsAsync()
        {
            memoryCache.Set(CacheProductKey, await productRepository.GetAll().ToListAsync());
        }
    }

}
