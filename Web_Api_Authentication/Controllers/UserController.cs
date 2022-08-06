using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Web_Api_Authentication.Interfaces.Services;
using Web_Api_Authentication.Models;
using Web_Api_Authentication.ViewModels;

namespace Web_Api_Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("/get-token")]
        public async Task<IActionResult> GetToken(LoginModel model)
        {
            var response = await _service.GetToken(model);

            if (IsHttpCodeOk(response))
                return Ok(response.Content);

            return BadRequest(response.Content);
        }

        [HttpGet]
        [Route("/get-all-users")]
        public async Task<IActionResult> GetAllUsers(string token)
        {
            var response = await _service.GetAllUsers(token);
            if (IsHttpCodeOk(response))
                return Ok(response.Content);

            return BadRequest(response.Content);
        }

        [HttpGet]
        [Route("/get-user-by-id")]
        public async Task<IActionResult> GetUserByCode(string token, long codigo)
        {
            var response = await _service.GetUserByCode(token, codigo);
            
            if (IsHttpCodeOk(response))
                return Ok(response.Content);

            return BadRequest(response.Content);
        }

        [HttpPost]
        [Route("/post-user")]
        public async Task<IActionResult> PostUser(string token, UserViewModel model)
        {
           RestResponse? response = await _service.PostUser(token, model);

            if (IsHttpCodeOk(response))
                return Ok(response.Content);

            return BadRequest(response.Content);
        }

        private bool IsHttpCodeOk(RestResponse response){
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                 return true;

             return false;

        }
    }
}