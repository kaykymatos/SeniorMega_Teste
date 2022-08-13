using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Web_Api_Authentication.Interfaces.Services;
using Web_Api_Authentication.Models;
using Web_Api_Authentication.ViewModels;

namespace Web_Api_Authentication.Controllers
{
    [ApiController]
    [AllowAnonymous]
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
            if (IsResponseNull(response))
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet]
        [Route("/get-user-by-id")]
        public async Task<IActionResult> GetUserByCode(string token, long codigo)
        {
            var response = await _service.GetUserByCode(token, codigo);

            if (IsResponseNull(response))
                return Ok(response);

            return NotFound(response);
        }

        [HttpPost]
        [Route("/post-user")]
        public async Task<IActionResult> PostUser(string token, UserModel model)
        {
            var response = await _service.PostUser(token, model);

            if (IsHttpCodeOk(response))
                return Ok(response.Content);

            return BadRequest(response.Content);
        }

        private bool IsResponseNull(List<UserEntityModel> response)
        {
            if (response.Count >= 1)
                return true;

            return false;

        }
        private bool IsHttpCodeOk(RestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;

        }
    }
}