using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Core.Dtos
{
    public class ErrorViewModel
    {
        //mvc katmannı error viewmodel oluşturuldu
        public List<string> Errors { get; set; } = new List<string>(); //add işlemini gerçekleştirmek için   
    }
}
