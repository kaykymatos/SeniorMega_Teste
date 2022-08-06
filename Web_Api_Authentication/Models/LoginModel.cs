using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api_Authentication.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}