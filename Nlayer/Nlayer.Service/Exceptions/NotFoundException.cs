using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Service.Exceptions
{
    public class NotFoundException:Exception
    {
        //eğer o data veritabanında yoksa onun için bir notfoundexception sınıfı oluşturdum
        public NotFoundException(string message):base(message)
        {
            
        }
    }
}
