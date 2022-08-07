using System.Globalization;

namespace Web_Api_Authentication.ViewModels
{
    public class UserModel
    {
        public string Nome { get; set; } = default!;
        private string Email { get; set; } = default!;
        public DateTime Data_Nascimento { get; set; }
        private DateTime Data_Criacao { get; set; }

        public UserModel(string nome, DateTime nascimento)
        {
            DateTime dateFormat = DateTime.ParseExact(nascimento.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            string dateToString = dateFormat.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            var dataCriacao = DateTime.Parse(dateToString);

            Data_Nascimento = dataCriacao;
            Data_Criacao = DateTime.UtcNow;
            Nome = nome;
            Email = $"{nome}@gmail.com";
        }

    }
}