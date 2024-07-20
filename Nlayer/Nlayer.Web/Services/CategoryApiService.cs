using Nlayer.Core.Dtos;

namespace Nlayer.Web.Services
{
    public class CategoryApiService
    {
        /// <summary>
        /// <see cref="CategoryApiService"/> sınıfının yeni bir örneğini başlatır.
        /// </summary>
        /// <param name="httpClient">İstek yapmak için kullanılacak HTTP istemcisi.</param>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Belirtilen HTTP istemcisi ile <see cref="CategoryApiService"/> sınıfının yeni bir örneğini başlatır.
        /// </summary>
        /// <param name="httpClient">İstek yapmak için kullanılacak HTTP istemcisi.</param>
        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Tüm kategorileri asenkron olarak getirir.
        /// </summary>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu bir <see cref="CategoryDto"/> listesi içerir.</returns>
        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<CategoryDto>>>("categories");
            return response.Data;
        }
    }
}
