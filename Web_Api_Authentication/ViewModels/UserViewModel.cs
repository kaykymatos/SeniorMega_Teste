using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api_Authentication.ViewModels
{
    public class UserViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime Data_Nascimento { get; set; }
    }
}