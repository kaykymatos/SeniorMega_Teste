using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Web_Api_Authentication.Models;
using Web_Api_Authentication.ViewModels;

namespace Web_Api_Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private const string URL_EXTERNAL_API = "http://168.138.231.9:10666";

        public UserController()
        {
        }



        [HttpPost]
        public async Task<IActionResult> GetAllUsers(UserViewModel model)
        {
            var client = new RestClient($"{URL_EXTERNAL_API}");
            var request = new RestRequest("cadastro/", Method.Post).AddHeader("Authorization", "Bearer ");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new UserViewModel
            {
                Nome = model.Nome,
                Email = model.Email,
                Data_Nascimento = model.Data_Nascimento
            });
            var response = await client.ExecuteAsync(request);
           

            return Ok(response);
        }



    }
}