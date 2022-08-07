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
        public async Task<RestResponse> GetAllUsers(string token)
        {
            var client = new RestClient(URL_EXTERNAL_API);
            var request = new RestRequest("cadastro/", Method.Get)
            .AddHeader("Authorization", $"Bearer {token}");
            request.RequestFormat = DataFormat.Json;
            
            var response = await client.ExecuteAsync(request);

            return response;
        }

        public async Task<RestResponse> GetToken(LoginModel model)
        {
            var client = new RestClient(URL_EXTERNAL_API);
            client.Authenticator = new HttpBasicAuthenticator(model.UserName, model.Password);
            var request = new RestRequest("get-token/", Method.Get);
            request.RequestFormat = DataFormat.Json;
            
            var response = await client.ExecuteAsync(request);
            
            return response;
        }

        public async Task<RestResponse> GetUserByCode(string token, long codigo)
        {
            var client = new RestClient(URL_EXTERNAL_API);
            var request = new RestRequest($"cadastro/{codigo}", Method.Get)
            .AddHeader("Authorization", $"Bearer {token}");
            request.RequestFormat = DataFormat.Json;
            
            var response = await client.ExecuteAsync(request);

            return response;
        }
        public async Task<RestResponse> PostUser(string token, UserModel model)
        {
            model = new UserModel(model.Nome, model.Data_Nascimento);
            var client = new RestClient(URL_EXTERNAL_API);
            var request = new RestRequest("cadastro", Method.Post)
                .AddJsonBody(model)
                .AddHeader("Authorization", $"Bearer {token}");
           
            var response = await client.ExecuteAsync(request);

            return response;
        }
    }
}