using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Api_Authentication.Models
{
    [Table("Users")]
    public class UserModel
    {
        [Key]
        public long Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime Data_Nascimento { get; set; }
        public DateTime Data_Criacao { get; set; }

    }
}