using RestSharp;
using Web_Api_Authentication.Models;
using Web_Api_Authentication.ViewModels;

namespace Web_Api_Authentication.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserEntityModel>> GetAllUsers(string token);
        Task<List<UserEntityModel>> GetUserByCode(long codigo, string token);
        Task<RestResponse> GetToken(LoginModel model);
        Task<string> PostUser(string token, UserModel model);


    }
}