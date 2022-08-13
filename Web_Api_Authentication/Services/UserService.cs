using System.Text.Json.Nodes;
using RestSharp;
using RestSharp.Authenticators;
using Web_Api_Authentication.Interfaces.Repository;
using Web_Api_Authentication.Interfaces.Services;
using Web_Api_Authentication.Models;
using Web_Api_Authentication.ViewModels;

namespace Web_Api_Authentication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private const string URL_EXTERNAL_API = "http://168.138.231.9:10666";

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<UserEntityModel>> GetAllUsers(string token)
        {
            var client = new RestClient(URL_EXTERNAL_API);
            var request = new RestRequest("cadastro/", Method.Get)
            .AddHeader("Authorization", $"Bearer {token}");

            var response = await client.GetAsync<List<UserEntityModel>>(request);
            WriteTxtFileWithDatabase(response, "GetllData");
            return response;

        }

        public async Task<RestResponse> GetToken(LoginModel model)
        {
            var client = new RestClient(URL_EXTERNAL_API);
            client.Authenticator = new HttpBasicAuthenticator(model.UserName, model.Password);
            var request = new RestRequest("get-token/", Method.Get);

            var response = await client.ExecuteAsync(request);

            return response;
        }

        public async Task<List<UserEntityModel>> GetUserByCode(string token, long codigo)
        {
            var client = new RestClient(URL_EXTERNAL_API);
            var request = new RestRequest($"cadastro/{codigo}", Method.Get)
            .AddHeader("Authorization", $"Bearer {token}");

            var response = await client.GetAsync<List<UserEntityModel>>(request);
            WriteTxtFileWithDatabase(response, "GetOneUser");
            return response;
        }
        public async Task<RestResponse> PostUser(string token, UserModel model)
        {

            var userModelNew = ConvertUserModelToUserEntity(model);
            var client = new RestClient(URL_EXTERNAL_API);
            var request = new RestRequest("cadastro", Method.Post)
                .AddHeader("Authorization", $"Bearer {token}")
                .AddHeader(URL_EXTERNAL_API, URL_EXTERNAL_API)
                .AddJsonBody(model);

            var response = await client.PostAsync(request);

            return response;
        }

        public UserEntityModel ConvertUserModelToUserEntity(UserModel model)
        {
            var newModel = new UserEntityModel
            {
                Nome = model.Nome,
                Email = model.Email,
                Data_Nascimento = model.Data_Nascimento,
                Data_Criacao = DateTime.Now
            };
            return newModel;
        }
        public void WriteTxtFileWithDatabase(List<UserEntityModel> response, String nameTxt)
        {
            using (TextWriter tw = new StreamWriter($"{nameTxt}.txt"))
            {
                foreach (var item in response)
                {
                    tw.WriteLine(string.Format(@$"
                    Código: {item.Codigo}, 
                    Nome: {item.Nome},
                    E-mail: {item.Email}, 
                    Data de Nascimento: {item.Data_Nascimento} 
                    Data de Criação: {item.Data_Criacao}"));
                }
            }
        }
    }
}