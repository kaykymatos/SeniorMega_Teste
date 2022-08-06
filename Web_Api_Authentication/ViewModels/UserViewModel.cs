using System.ComponentModel.DataAnnotations;

namespace Web_Api_Authentication.ViewModels
{
    public class UserViewModel
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime Data_Nascimento { get; set; }

    }
}