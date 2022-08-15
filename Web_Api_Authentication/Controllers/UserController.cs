using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Web_Api_Authentication.ExternalErrors;
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
            RestResponse response = await _service.GetToken(model);

            if (IsHttpCodeOk(response))
                return Ok(response.Content);

            return BadRequest(response.Content);
        }

        [HttpGet]
        [Route("/get-all-users")]
        public async Task<IActionResult> GetAllUsers(string token)
        {
            RestResponse response = await _service.GetAllUsers(token);
            if (_service.IsResponseAnErrorMessage(response.Content))
                return Ok(response.Content);

            return BadRequest(response.Content);
        }

        [HttpGet]
        [Route("/get-user-by-id")]
        public async Task<IActionResult> GetUserByCode(string token, long codigo)
        {
            RestResponse response = await _service.GetUserByCode(codigo, token);

            if (_service.IsResponseAnErrorMessage(response.Content!))
                return Ok(response.Content!);

            return NotFound(response.Content);
        }

        [HttpPost]
        [Route("/post-user")]
        public IActionResult PostUser(string token, UserModel model)
        {
            object response = _service.PostUser(token, model);

            if (_service.IsResponseAnErrorMessage(response.ToString()!))
                return Ok(response);

            return BadRequest(response);

        }
        private bool IsHttpCodeOk(RestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;

        }
    }
}