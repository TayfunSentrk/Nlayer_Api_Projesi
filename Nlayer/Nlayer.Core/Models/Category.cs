using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Core.Models
{
    public class Category : BaseEntity
    {
        //Category entity'si oluşturuldu.Base entity'den miras aldı
        public string Name { get; set; }
        // Category ile product arasında bire çok ilişki olduğu için Products Navigation Property oluşturuldu
        public ICollection<Product> Products { get; set; }
    }
}
