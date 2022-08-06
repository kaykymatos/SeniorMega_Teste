using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Web_Api_Authentication.Data;
using Web_Api_Authentication.Interfaces.Repository;
using Web_Api_Authentication.Models;

namespace Web_Api_Authentication.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string URL_EXTERNAL_API = "http://168.138.231.9:10666";
        private readonly ApiDbContext _context;
        public UserRepository(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<RestResponse> GetAllUsers(string token)
        {
            var client = new RestClient($"{URL_EXTERNAL_API}");
            var request = new RestRequest("cadastro/", Method.Get).AddHeader("Authorization", $"Bearer {token}");
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
            var client = new RestClient($"{URL_EXTERNAL_API}");
            var request = new RestRequest($"cadastro/{codigo}", Method.Get).AddHeader("Authorization", $"Bearer {token}");
            request.RequestFormat = DataFormat.Json;

            var response = await client.ExecuteAsync(request);
           
            return response;
        }

        public async Task<UserModel> PostUser(string token, UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}