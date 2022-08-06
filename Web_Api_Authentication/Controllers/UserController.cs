using Microsoft.AspNetCore.Mvc;
using Web_Api_Authentication.Interfaces.Services;
using Web_Api_Authentication.Models;
using Web_Api_Authentication.ViewModels;

namespace Web_Api_Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private const string URL_EXTERNAL_API = "http://168.138.231.9:10666";
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

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return Ok(response.Content);

            return BadRequest(response.Content);
        }

        [HttpGet]
        [Route("/get-all-users")]
        public async Task<IActionResult> GetAllUsers(string token)
        {
            var response = await _service.GetAllUsers(token);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return Ok(response.Content);

            return BadRequest(response.Content);
        }

        [HttpGet]
        [Route("/get-user-by-id")]
        public async Task<IActionResult> GetUserByCode(string token, long codigo)
        {
            var response = await _service.GetUserByCode(token, codigo);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return Ok(response.Content);

            return BadRequest(response.Content);
        }

        [HttpPost]
        [Route("/post-user")]
        public async Task<IActionResult> PostUser(UserViewModel model, string token)
        {
            return Ok("");
        }



    }
}