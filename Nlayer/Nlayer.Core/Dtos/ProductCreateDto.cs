using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Core.Dtos
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        // stock property oluşturuldu
        public int Stock { get; set; }
        // Price  property oluşturuldu parasal birim olduğu için decimak oluşturuldu
        public decimal Price { get; set; }
        // Category ile product arasında bire çok ilişki olduğu için CategoryId property eklendi
        public int CategoryId { get; set; }
        // Category ile product arasında bire çok ilişki olduğu için Category Navigation Property oluşturuldu
    }
}
