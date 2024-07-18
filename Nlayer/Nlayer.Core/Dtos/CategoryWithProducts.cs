using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Core.Dtos
{
    public class CategoryWithProducts:CategoryDto
    {
      
        public List<ProductDto> ProductDtos { get; set; }
    }
}
