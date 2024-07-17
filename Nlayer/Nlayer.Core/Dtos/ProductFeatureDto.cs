using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Core.Dtos
{
    public class ProductFeatureDto
    {
        //Primary key property'si
        public int Id { get; set; }
        //Renk property'si
        public string Color { get; set; }

        //Uzunluk property'si
        public int Height { get; set; }

        //ağırlık property'si
        public int Weight { get; set; }

        //product ile bağlantı olduğu için product Id verildi
        public int ProductId { get; set; }
    }
}
