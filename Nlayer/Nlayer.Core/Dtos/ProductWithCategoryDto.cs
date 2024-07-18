using Nlayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Core.Dtos
{
    public class ProductWithCategoryDto:ProductDto
    {
        //ProductDto 'dan miras alındı .Categori'yi çekmek için CategoryDto'dakiler class olarak oluşturuldu
        public CategoryDto CategoryDto { get; set; }
    }
}
