using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Api_Authentication.Models
{
    [Table("Users")]
    public class UserEntityModel
    {
        [Key]
        public long Codigo { get; set; }
        public string Nome { get; set; }= default!;        
        [DataType(DataType.DateTime)]
        public DateTime Data_Nascimento { get; set; }        
    }
}