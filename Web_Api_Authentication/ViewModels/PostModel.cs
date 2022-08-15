using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web_Api_Authentication.ViewModels
{
    public class PostModel
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; } = default!;
        [JsonPropertyName("email")]
        public string Email { get; set; } = default!;
        [JsonPropertyName("data_nascimento")]
        public string Data_Nascimento { get; set; } = default!;
        [JsonPropertyName("data_criacao")]
        public string Data_Criacao { get; set; } = default!;
    }
}