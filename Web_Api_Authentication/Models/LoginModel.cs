namespace Web_Api_Authentication.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }= default!;
        public string Password { get; set; }= default!;
    }
}