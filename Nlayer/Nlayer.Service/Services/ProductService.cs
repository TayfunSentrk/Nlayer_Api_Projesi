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
    public class ProductService : Service<Product>, IProductService
    {
        // Bu, ProductService sınıfında kullanılan özel alanları tanımlar.
        private readonly IProductRepository _repository;
        private readonly IMapper mapper;

        // ProductService sınıfının constructor'ı. Bu, sınıfın bir örneği oluşturulduğunda çağrılır.
        public ProductService(IGenericRepository<Product> genericRepository, IUnitOfWork unitOfWork, IProductRepository repository, IMapper mapper)
            : base(genericRepository, unitOfWork) // Base sınıfının constructor'ını çağırır.
        {
            // _repository alanını parametre olarak verilen repository ile başlatır.
            _repository = repository;
            // mapper alanını parametre olarak verilen mapper ile başlatır.
            this.mapper = mapper;
        }
        // Asenkron olarak ürünleri kategorileri ile birlikte getirir.
        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategoryAsync()
        {
            // _repository.GetProductWithCategory() metodunu çağırır ve sonucu bekler.
            // Sonucu, List<ProductWithCategoryDto> türüne dönüştürmek için mapper kullanır ve döner.
            // Dönüş değerini CustomResponseDto ile sararak, başarı durumu ve HTTP 200 durum kodu ile birlikte döner.

            var a= await _repository.GetProductWithCategory();
            var b = mapper.Map<List<ProductWithCategoryDto>>(await _repository.GetProductWithCategory());
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(
                200,
                mapper.Map<List<ProductWithCategoryDto>>(await _repository.GetProductWithCategory())
            );

        }
    }
}
