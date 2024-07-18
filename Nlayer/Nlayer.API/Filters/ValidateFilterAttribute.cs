using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nlayer.Core.Dtos;

namespace Nlayer.API.Filters
{
    /// <summary>
    /// ValidateFilterAttribute sınıfı, aksiyon öncesinde model doğrulamasını kontrol eden bir filtre uygular.
    /// ActionFilterAttribute sınıfından türetilmiştir.
    /// </summary>
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// OnActionExecuting metodu, aksiyon metodunun yürütülmesinden önce çağrılır.
        /// ModelState geçerli değilse, hata mesajlarını toplayıp kötü istek (400) sonucu döner.
        /// </summary>
        /// <param name="context">Aksiyon yürütme bağlamı</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // ModelState geçerli değilse
            if (!context.ModelState.IsValid)
            {
                // ModelState içindeki tüm hata mesajlarını toplar.
                var errors = context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                // Hata mesajlarını içeren bir kötü istek (400) sonucu döner.
                context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400, errors));
            }
        }
    }
}
