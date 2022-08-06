using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Web_Api_Authentication.Models;

namespace Web_Api_Authentication.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<RestResponse> GetAllUsers(string token);
        Task<RestResponse> GetUserByCode(string token, long codigo);
        Task<RestResponse> GetToken(LoginModel model);
        Task<UserModel> PostUser(string token, UserModel model);
    }
}