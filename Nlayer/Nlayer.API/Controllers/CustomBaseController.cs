using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nlayer.Core.Dtos;

namespace Nlayer.API.Controllers
{

    /// <summary>
    /// Özel taban kontrolcü sınıfı
    /// </summary>
    public class CustomBaseController : ControllerBase
    {
        /// <summary>
        /// Bir end point olmadığını göstermek için NonAction olarak işaretlenmiş metot
        /// </summary>
        /// <typeparam name="T">Dönüş tipi</typeparam>
        /// <param name="response">CustomResponseDto türünde yanıt</param>
        /// <returns>IActionResult türünde yanıt</returns>
        [NonAction] // bir end point olmadığını göstermek için
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            // Eğer yanıtın durum kodu 204 ise, içeriği boş bir yanıt döner
            if (response.StatusCode == 204)
            {
                return new ObjectResult(null) { StatusCode = response.StatusCode };
            }

            // Yanıtı ve durum kodunu döner
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
