using System.Text.Json;
using System.Text.Json.Serialization;
using RestSharp;
using RestSharp.Authenticators;
using Web_Api_Authentication.Interfaces.Repository;
using Web_Api_Authentication.Interfaces.Services;
using Web_Api_Authentication.Models;

namespace Web_Api_Authentication.Services
{
    public class UserService : IUserService
    {
        private const string URL_EXTERNAL_API = "http://168.138.231.9:10666";
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<RestResponse> GetAllUsers(string token)
        {
            var response = await _repository.GetAllUsers(token);
            return response;
        }

        public async Task<RestResponse> GetToken(LoginModel model)
        {
            var response = await _repository.GetToken(model);
            return response;
        }

        public async Task<RestResponse> GetUserByCode(string token, long codigo)
        {
            var response = await _repository.GetUserByCode(token, codigo);

            return response;
        }

        public Task<UserModel> PostUser(string token, UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}