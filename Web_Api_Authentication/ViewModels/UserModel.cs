using System.Globalization;

namespace Web_Api_Authentication.ViewModels
{
    public class UserModel
    {
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime Data_Nascimento { get; set; }

    }
}