using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api_Authentication.Models
{
    [Table("user")]
    public class UserModel
    {
        [Key]
        public long Codigo { get; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime Data_Nascimento { get; set; }

    }
}