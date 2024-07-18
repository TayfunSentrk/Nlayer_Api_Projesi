using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Service.Exceptions
{
    public class ClientSideException:Exception
    {
        public ClientSideException(string message):base(message)
        {
            //Burda Response hata mesajı alacağımız için burdan bir string message tanımladım bunu base(Exceptiona) gönderdim
        }
    }
}
