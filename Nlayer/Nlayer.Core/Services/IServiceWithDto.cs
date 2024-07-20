using Nlayer.Core.Dtos;
using Nlayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Core.Services
{
    /// <summary>
    /// Dto ile çalışan hizmet arayüzü.
    /// </summary>
    /// <typeparam name="TEntity">Temel varlık türü. <see cref="BaseEntity"/>'den türetilmeli ve yeni bir örneklenebilir olmalı.</typeparam>
    /// <typeparam name="Dto">Veri transfer nesnesi türü. Sınıf olmalı.</typeparam>
    public interface IServiceWithDto<TEntity, Dto> where TEntity : BaseEntity, new() where Dto : class
    {
        /// <summary>
        /// Belirtilen kimliğe sahip öğeyi asenkron olarak getirir.
        /// </summary>
        /// <param name="id">Getirilecek öğenin kimliği.</param>
        /// <returns>Belirtilen kimliğe sahip öğeyi içeren bir <see cref="CustomResponseDto{Dto}"/>.</returns>
        Task<CustomResponseDto<Dto>> GetByIdAsync(int id);

        /// <summary>
        /// Tüm öğeleri asenkron olarak getirir.
        /// </summary>
        /// <returns>Öğeleri içeren bir <see cref="CustomResponseDto{IEnumerable{Dto}}"/>.</returns>
        Task<CustomResponseDto<IEnumerable<Dto>>> GetAll();

        /// <summary>
        /// Belirtilen ifadeye uyan öğeleri asenkron olarak getirir.
        /// </summary>
        /// <param name="expression">Uygulanacak ifade.</param>
        /// <returns>Belirtilen ifadeye uyan öğeleri içeren bir <see cref="CustomResponseDto{IEnumerable{Dto}}"/>.</returns>
        Task<CustomResponseDto<IEnumerable<Dto>>> Where(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Yeni bir öğeyi asenkron olarak ekler.
        /// </summary>
        /// <param name="dto">Eklenecek öğe.</param>
        /// <returns>Eklenen öğeyi içeren bir <see cref="CustomResponseDto{Dto}"/>.</returns>
        Task<CustomResponseDto<Dto>> AddAsync(Dto dto);

        /// <summary>
        /// Birden fazla öğeyi asenkron olarak ekler.
        /// </summary>
        /// <param name="dtos">Eklenecek öğeler.</param>
        /// <returns>Eklenen öğeleri içeren bir <see cref="CustomResponseDto{IEnumerable{Dto}}"/>.</returns>
        Task<CustomResponseDto<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtos);

        /// <summary>
        /// Belirtilen ifadeye uyan bir öğe olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="expression">Uygulanacak ifade.</param>
        /// <returns>Belirtilen ifadeye uyan bir öğe varsa true, yoksa false değerini içeren bir <see cref="CustomResponseDto{bool}"/>.</returns>
        Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Mevcut bir öğeyi asenkron olarak günceller.
        /// </summary>
        /// <param name="dto">Güncellenecek öğe.</param>
        /// <returns>Güncelleme işleminin sonucunu içeren bir <see cref="CustomResponseDto{NoContentDto}"/>.</returns>
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(Dto dto);

        /// <summary>
        /// Belirtilen kimliğe sahip öğeyi asenkron olarak kaldırır.
        /// </summary>
        /// <param name="id">Kaldırılacak öğenin kimliği.</param>
        /// <returns>Kaldırma işleminin sonucunu içeren bir <see cref="CustomResponseDto{NoContentDto}"/>.</returns>
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id);

        /// <summary>
        /// Birden fazla öğeyi asenkron olarak kaldırır.
        /// </summary>
        /// <param name="ids">Kaldırılacak öğelerin kimlikleri.</param>
        /// <returns>Kaldırma işleminin sonucunu içeren bir <see cref="CustomResponseDto{NoContentDto}"/>.</returns>
        Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<int> ids);
    }

}
