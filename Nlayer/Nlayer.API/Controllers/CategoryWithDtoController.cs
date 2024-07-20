using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nlayer.Core.Dtos;
using Nlayer.Core.Models;
using Nlayer.Core.Services;

namespace Nlayer.API.Controllers
{

    public class CategoryWithDtoController : CustomBaseController
    {
        private readonly IServiceWithDto<Category, CategoryDto> serviceWithDto;

        public CategoryWithDtoController(IServiceWithDto<Category, CategoryDto> serviceWithDto)
        {
            this.serviceWithDto = serviceWithDto;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory()
        {
            return CreateActionResult(await serviceWithDto.GetAll());
        }

        [HttpPost]

        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            return CreateActionResult(await serviceWithDto.AddAsync(categoryDto));
        }
    }
}
