using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nlayer.Core.Dtos
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }

        public List<string> Errors { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; } //bunu cliente dönmeme gerek yok zaten biliyolarlar


        /// <summary>
        /// Belirtilen statusCode ve T tipinde veri alır ve CustomResponseDto<T> türünde bir nesne döner.
        /// </summary>
        /// <param name="statusCode">HTTP durum kodu.</param>
        /// <param name="data">T tipinde veri.</param>
        /// <returns>CustomResponseDto<T> türünde bir nesne döner.</returns>
        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T>() { Data = data, StatusCode = statusCode, Errors = null };
        }

        /// <summary>
        /// Belirtilen statusCode ile CustomResponseDto<T> türünde bir nesne döner.
        /// </summary>
        /// <param name="statusCode">HTTP durum kodu.</param>
        /// <returns>CustomResponseDto<T> türünde bir nesne döner.</returns>
        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T>()
            {
                StatusCode = statusCode
            };
        }

        /// <summary>
        /// Belirtilen statusCode ve hata listesi ile CustomResponseDto<T> türünde bir nesne döner.
        /// </summary>
        /// <param name="statusCode">HTTP durum kodu.</param>
        /// <param name="errors">Hata mesajları listesi.</param>
        /// <returns>CustomResponseDto<T> türünde bir nesne döner.</returns>
        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T>()
            {
                StatusCode = statusCode,
                Errors = errors
            };
        }

        /// <summary>
        /// Belirtilen statusCode ve tek bir hata mesajı ile CustomResponseDto<T> türünde bir nesne döner.
        /// </summary>
        /// <param name="statusCode">HTTP durum kodu.</param>
        /// <param name="error">Tek hata mesajı.</param>
        /// <returns>CustomResponseDto<T> türünde bir nesne döner.</returns>
        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T>()
            {
                StatusCode = statusCode,
                Errors = new List<string> { error }
            };
        }




    }
}
    
