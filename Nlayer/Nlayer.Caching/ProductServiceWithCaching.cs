using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Nlayer.Core.Dtos;
using Nlayer.Core.Models;
using Nlayer.Core.Repositories;
using Nlayer.Core.Services;
using Nlayer.Core.UnitOfWorks;
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
        //decorator desing pattern olarak yapıyı değiştirmemek için

        private const string CacheProductKey = "productsCache";


        private readonly IMapper mapper; // dönüştürme işlemleri için

        private readonly IMemoryCache memoryCache; //cacheleme için gerekli yapı
        private readonly IProductRepository productRepository; //productrepository veritabanı işlemleri için

        private readonly IUnitOfWork unitOfWork;//veritabanına yansıtmak için

        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.memoryCache = memoryCache;
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;

            if(!memoryCache.TryGetValue(CacheProductKey,out _))
            {
                memoryCache.Set(CacheProductKey, productRepository.GetAll().ToList()); //eğer memorycache herhangi bir değer sahip depilse _ ile memory'de kaydolmasını engelleiyorum
                //daha sonra liste repository'de olanları liste şeklinde kaydediyorum
            } // cache'te değer var sa girmesine gerek yok
        }

        public Task<Product> AddAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<Product> entites)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
