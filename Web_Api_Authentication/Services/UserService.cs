using System.Text.Json;
using System.Text.Json.Serialization;
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

        public async Task<RestResponse> PostUser(string token, UserViewModel model)
        {
            var userModel = UserViewModeToUserModel(model);
            var response = await _repository.PostUser(token, userModel);

            return response;
        }
        public UserModel UserViewModeToUserModel(UserViewModel viewModel)
        {
            UserModel model = new UserModel();
            model.Nome = viewModel.Nome;
            model.Email = viewModel.Email;
            model.Data_Nascimento = viewModel.Data_Nascimento;
            model.Data_Criacao = DateTime.Now;
            return model;
        }
    }
}