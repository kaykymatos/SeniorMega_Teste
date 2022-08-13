using System;
using System.ComponentModel.DataAnnotations;

namespace Web_Api_Authentication.Models
{
    public class UserEntityModel
    {
        public long Codigo { get; set; }
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime Data_Nascimento { get; set; }
        public DateTime Data_Criacao { get; set; }
    }
}