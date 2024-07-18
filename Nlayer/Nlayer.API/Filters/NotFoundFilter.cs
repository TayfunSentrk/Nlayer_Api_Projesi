using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nlayer.Core.Dtos;
using Nlayer.Core.Models;
using Nlayer.Core.Services;

namespace Nlayer.API.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter  where T :BaseEntity,new()
    {

        private readonly IService<T> _service; //entity var mı yok mu kontrol etmek için

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        //burda NotFound Filter BaseEntity türeten entityler için olduğu için ve new'lenenlerden olması gerektiği belirtildi
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) //next olmasının sebebi herhangi bir hata olmazsa response devam yoluna devam edicek
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();//property'deki ilk değer yani id denk geliyor

            if (idValue == null)
            {
                await next.Invoke();//  yoluna devam et ama aşağı inme anlamında
                return;
            }

           var id=(int)idValue;//int kastetmek için yaptım

            var anyEntity = await _service.AnyAsync(x=>x.Id==id);//entity var mı yok mu kontrol etmek için

            if (anyEntity) //eğer entity var ise 
            {
                await next.Invoke(); //bir sonraki adıma geçsin
                return; //alt koda inmesin
            }

            context.Result=new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404,$"{typeof(T).Name} is not found")); //burdan bir response dönüyor hata kodu ve t tipine göre name'yi alıyor
        }
    }
}
