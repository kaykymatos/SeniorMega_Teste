using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Api_Authentication.Models
{
    [Table("Users_Table")]
    public class UserEntityModel
    {
        [Key]
        public long Codigo { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(250)]
        public string Nome { get; set; } = default!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        [DataType(DataType.Date)]
        public DateTime Data_Nascimento { get; set; } 
        public DateTime Data_Criacao { get; set; }
    }
}