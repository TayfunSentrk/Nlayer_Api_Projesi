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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Service.Services
{
    /// <summary>
    /// ServiceWithDto sınıfı, Dto nesneleri ile çalışmak için genel hizmet metodları sağlar.
    /// </summary>
    /// <typeparam name="TEntity">Veri tabanı varlık sınıfı türü</typeparam>
    /// <typeparam name="Dto">DTO (Data Transfer Object) sınıfı türü</typeparam>
    public class ServiceWithDto<TEntity, Dto> : IServiceWithDto<TEntity, Dto> where TEntity : BaseEntity, new() where Dto : class
    {
        private readonly IGenericRepository<TEntity> genericRepository;
        protected readonly IUnitOfWork unitOfWork; //miras alan sınıflarda kullanmak için
        protected readonly IMapper mapper; //miras alan sınıflarda kullanmak için

        /// <summary>
        /// ServiceWithDto sınıfının yapıcı metodu.
        /// </summary>
        /// <param name="genericRepository">Generic repository nesnesi</param>
        /// <param name="unitOfWork">Unit of Work nesnesi</param>
        /// <param name="mapper">Mapper nesnesi</param>
        public ServiceWithDto(IGenericRepository<TEntity> genericRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.genericRepository = genericRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        /// <summary>
        /// Yeni bir DTO ekler.
        /// </summary>
        /// <param name="dto">Eklenecek DTO nesnesi</param>
        /// <returns>Eklenen DTO nesnesini içeren özel yanıt DTO'su</returns>
        public async Task<CustomResponseDto<Dto>> AddAsync(Dto dto)
        {
            var newEntity = mapper.Map<TEntity>(dto);
            await genericRepository.AddAsync(newEntity);
            await unitOfWork.CommitAsync();
            var newDto = mapper.Map<Dto>(newEntity);
            return CustomResponseDto<Dto>.Success(StatusCodes.Status201Created, newDto);
        }

        /// <summary>
        /// Bir dizi DTO nesnesi ekler.
        /// </summary>
        /// <param name="dtos">Eklenecek DTO nesneleri dizisi</param>
        /// <returns>Eklenen DTO nesnelerini içeren özel yanıt DTO'su</returns>
        public async Task<CustomResponseDto<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtos)
        {
            var newEntities = mapper.Map<IEnumerable<TEntity>>(dtos);
            await genericRepository.AddRangeAsync(newEntities);
            await unitOfWork.CommitAsync();
            var newDtos = mapper.Map<IEnumerable<Dto>>(newEntities);
            return CustomResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status201Created, newDtos);
        }

        /// <summary>
        /// Belirtilen koşula göre herhangi bir kaydın var olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="expression">Kontrol edilecek ifade</param>
        /// <returns>Koşula uyan herhangi bir kaydın olup olmadığını içeren özel yanıt DTO'su</returns>
        public async Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            var hasResult = await genericRepository.AnyAsync(expression);
            return CustomResponseDto<bool>.Success(StatusCodes.Status200OK, hasResult);
        }

        /// <summary>
        /// Tüm DTO nesnelerini getirir.
        /// </summary>
        /// <returns>Tüm DTO nesnelerini içeren özel yanıt DTO'su</returns>
        public async Task<CustomResponseDto<IEnumerable<Dto>>> GetAll()
        {
            var listEntity = mapper.Map<IEnumerable<Dto>>(await genericRepository.GetAll().ToListAsync());
            return CustomResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, listEntity);
        }

        /// <summary>
        /// Belirtilen ID'ye sahip DTO nesnesini getirir.
        /// </summary>
        /// <param name="id">DTO nesnesinin ID'si</param>
        /// <returns>Belirtilen ID'ye sahip DTO nesnesini içeren özel yanıt DTO'su</returns>
        public async Task<CustomResponseDto<Dto>> GetByIdAsync(int id)
        {
            var dto = mapper.Map<Dto>(await genericRepository.GetByIdAsync(id));
            return CustomResponseDto<Dto>.Success(StatusCodes.Status200OK, dto);
        }

        /// <summary>
        /// Belirtilen ID'ye sahip DTO nesnesini siler.
        /// </summary>
        /// <param name="id">Silinecek DTO nesnesinin ID'si</param>
        /// <returns>NoContent DTO'sunu içeren özel yanıt DTO'su</returns>
        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id)
        {
            var entity = await genericRepository.GetByIdAsync(id);
            genericRepository.Remove(entity);
            await unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Belirtilen ID'lere sahip DTO nesnelerini toplu olarak siler.
        /// </summary>
        /// <param name="ids">Silinecek DTO nesnelerinin ID'leri</param>
        /// <returns>NoContent DTO'sunu içeren özel yanıt DTO'su</returns>
        public async Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<int> ids)
        {
            var entities = await genericRepository.Where(x => ids.Contains(x.Id)).ToListAsync();
            genericRepository.RemoveRange(entities);
            await unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Bir DTO nesnesini günceller.
        /// </summary>
        /// <param name="dto">Güncellenecek DTO nesnesi</param>
        /// <returns>NoContent DTO'sunu içeren özel yanıt DTO'su</returns>
        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(Dto dto)
        {
            var entity = mapper.Map<TEntity>(dto);
            genericRepository.Update(entity);
            await unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Belirtilen koşula uyan DTO nesnelerini getirir.
        /// </summary>
        /// <param name="expression">Koşulu belirten ifade</param>
        /// <returns>Belirtilen koşula uyan DTO nesnelerini içeren özel yanıt DTO'su</returns>
        public async Task<CustomResponseDto<IEnumerable<Dto>>> Where(Expression<Func<TEntity, bool>> expression)
        {
            var dtos = mapper.Map<IEnumerable<Dto>>(await genericRepository.Where(expression).ToListAsync());
            return CustomResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, dtos);
        }
    }

}
