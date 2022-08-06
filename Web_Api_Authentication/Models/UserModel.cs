using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Web_Api_Authentication.Models
{
    [Table("Users")]
    public class UserModel
    {
        [Key]
        public long Codigo { get; set; }
        [Required]
        [MaxLength(250)]
        [MinLength(3)]
        public string Nome { get; set; }= string.Empty;
        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(250)]
        public string Email { get; set; }= string.Empty;
        [DataType(DataType.DateTime)]
        public DateTime Data_Nascimento { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Data_Criacao { get; set; }

    }
}