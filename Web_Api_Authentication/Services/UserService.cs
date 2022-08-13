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

            var verifyException = client.Execute<List<UserEntityModel>>(request).ErrorException;
            if (verifyException != null)
            {
                return new List<UserEntityModel>();

            }
            else
            {
                var response = await client.GetAsync<List<UserEntityModel>>(request);
                foreach (var item in response)
                {
                    var itemcCnvert = ConvertUserEntityModelToDatabase(item);
                    _repository.PostUser(item, itemcCnvert);
                }
                return response;
            }




        }

        public async Task<RestResponse> GetToken(LoginModel model)
        {
            model.UserName = "Kayky";
            model.Password = "04571082584";
            var client = new RestClient(URL_EXTERNAL_API);
            client.Authenticator = new HttpBasicAuthenticator(model.UserName, model.Password);
            var request = new RestRequest("get-token/", Method.Get);

            var response = await client.ExecuteAsync(request);

            return response;
        }

        public async Task<List<UserEntityModel>> GetUserByCode(long codigo, string token)
        {
            var client = new RestClient(URL_EXTERNAL_API);
            var request = new RestRequest($"cadastro/{codigo}", Method.Get)
            .AddHeader("Authorization", $"Bearer {token}");

            var verifyException = client.Execute<List<UserEntityModel>>(request).ErrorException;
            if (verifyException != null)
            {
                return new List<UserEntityModel>();

            }
            else
            {
                return await client.GetAsync<List<UserEntityModel>>(request);
            }
        }
        public async Task<RestResponse> PostUser(string token, UserModel model)
        {


            var client = new RestClient(URL_EXTERNAL_API);
            var request = new RestRequest("cadastro", Method.Post)
                .AddHeader("Authorization", $"Bearer {token}")
                .AddHeader(URL_EXTERNAL_API, URL_EXTERNAL_API)
                .AddJsonBody(model);

            var response = await client.PostAsync(request);

            return response;
        }

        public UserEntityModel ConvertUserEntityModelToDatabase(UserEntityModel model)
        {
            var newModel = new UserEntityModel
            {
                Nome = model.Nome,
                Email = model.Email,
                Data_Nascimento = model.Data_Nascimento,
                Data_Criacao = model.Data_Criacao
            };
            return newModel;
        }
    }
}