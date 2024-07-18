using Nlayer.Core.Dtos;
using Nlayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Core.Services
{
    public interface ICategoryService:IService<Category>
    {

        public Task<CustomResponseDto<CategoryWithProducts>> GetSingleCategoryByIdWithProductAsync(int categoryId);
    }
}
