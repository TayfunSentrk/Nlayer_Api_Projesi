using Microsoft.AspNetCore.Mvc;
using Nlayer.Core.Dtos;
using System.Net;
using System.Net.Http.Json;

namespace Nlayer.Web.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient; // HttpClient örneğini private readonly olarak tanımladık.

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient; // HttpClient örneğini yapıcı metod ile alıyoruz.
        }

        public async Task<List<ProductWithCategoryDto>> GetProductWithCategoryDtosAsync()
        {
            // products/GetProductWithCategory endpoint'ine GET isteği gönderiyoruz.
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductWithCategoryDto>>>("products/GetProductWithCategory");

            return response.Data; // Gelen yanıtın Data kısmını döndürüyoruz.
        }

        public async Task<ProductDto> SaveAsync(ProductDto product)
        {
            // products endpoint'ine POST isteği ile yeni bir ürün ekliyoruz.
            var response = await _httpClient.PostAsJsonAsync("products", product);
            if (!response.IsSuccessStatusCode) return null; // İstek başarısızsa null döndürüyoruz.

            // Yanıtın gövdesini CustomResponseDto<ProductDto> türünde okuyoruz.
            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductDto>>();
            return responseBody.Data; // Gelen yanıtın Data kısmını döndürüyoruz.
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            // Belirtilen id ile ürün bilgilerini alıyoruz.
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<ProductDto>>($"products/${id}");

            return response.Data; // Gelen yanıtın Data kısmını döndürüyoruz.
        }

        public async Task<bool> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            // products endpoint'ine PUT isteği ile ürün bilgilerini güncelliyoruz.
            var response = await _httpClient.PutAsJsonAsync("products", productUpdateDto);
            return response.IsSuccessStatusCode; // İstek başarılıysa true döndürüyoruz.
        }

        public async Task<bool> RemoveAsync(int id)
        {
            // Belirtilen id ile ürün bilgisini siliyoruz.
            var response = await _httpClient.DeleteAsync($"products/${id}");
            return response.IsSuccessStatusCode; // İstek başarılıysa true döndürüyoruz.
        }

    }
}
