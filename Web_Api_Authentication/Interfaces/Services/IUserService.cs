using RestSharp;
using Web_Api_Authentication.Models;
using Web_Api_Authentication.ViewModels;

namespace Web_Api_Authentication.Interfaces.Services
{
    public interface IUserService
    {
        Task<RestResponse> GetAllUsers(string token);
        Task<RestResponse> GetUserByCode(string token, long codigo);
        Task<RestResponse> GetToken(LoginModel model);
        Task<RestResponse> PostUser(string token, UserModel model);


    }
}