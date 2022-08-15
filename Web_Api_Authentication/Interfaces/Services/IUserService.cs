using RestSharp;
using Web_Api_Authentication.Models;
using Web_Api_Authentication.ViewModels;

namespace Web_Api_Authentication.Interfaces.Services
{
    public interface IUserService
    {
        Task<RestResponse> GetAllUsers(string token);
        Task<RestResponse> GetUserByCode(long codigo, string token);
        Task<RestResponse> GetToken(LoginModel model);
        object PostUser(string token, UserModel model);
        bool IsResponseAnErrorMessage(string response);


    }
}