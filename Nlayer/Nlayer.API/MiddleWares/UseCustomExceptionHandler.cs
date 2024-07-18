using Microsoft.AspNetCore.Diagnostics;
using Nlayer.Core.Dtos;
using Nlayer.Service.Exceptions;
using System.Text.Json;

namespace Nlayer.API.MiddleWares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();    //IExceptionHandlerFeature hataları yakalayacak olan interface

                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400, //client taraflıysa 400 olucak
                        _ => 500 //default olarak 500 
                    };

                    context.Response.StatusCode = statusCode;//statuscode setledik
                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));  //kendim middleware yazdığım için otomatik olarak json'e çevirmez ondan dolayı json döndürüyorum
                });//run sonlandırıcı middleware
            });
        }
    }
}
