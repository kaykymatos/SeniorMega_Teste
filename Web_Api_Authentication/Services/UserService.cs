using OfficeOpenXml;
using OfficeOpenXml.Style;
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
        private const string URL_EXTERNAL_API = "http://168.138.231.9:10666";
        private const string excelFilePath = "./Usuarios_API.xlsx";

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<UserEntityModel>> GetAllUsers(string token)
        {
            var client = new RestClient(URL_EXTERNAL_API);
            var request = new RestRequest("cadastro/", Method.Get)
            .AddHeader("Authorization", $"Bearer {token}");

            var verifyException = client.Execute<List<UserEntityModel>>(request).ErrorException;
            if (verifyException != null)
            {
                return new List<UserEntityModel>();

            }
            else
            {
                var response = await client.GetAsync<List<UserEntityModel>>(request);
                CreateExcelTable(response);
                foreach (var item in response)
                {
                    var itemConvert = ConvertUserEntityModelToDatabase(item);
                    _repository.PostUser(itemConvert);
                }
                return response;
            }
        }

        public async Task<RestResponse> GetToken(LoginModel model)
        {
            var client = new RestClient(URL_EXTERNAL_API);
            client.Authenticator = new HttpBasicAuthenticator(model.UserName, model.Password);
            var request = new RestRequest("get-token/", Method.Get);

            var response = await client.ExecuteAsync(request);

            return response;
        }

        public async Task<List<UserEntityModel>> GetUserByCode(long codigo, string token)
        {
            var client = new RestClient(URL_EXTERNAL_API);
            var request = new RestRequest($"cadastro/{codigo}", Method.Get)
            .AddHeader("Authorization", $"Bearer {token}");

            var verifyException = client.Execute<List<UserEntityModel>>(request).ErrorException;
            if (verifyException != null)
            {
                return new List<UserEntityModel>();

            }
            else
            {
                return await client.GetAsync<List<UserEntityModel>>(request);
            }
        }
        public async Task<RestResponse> PostUser(string token, UserModel model)
        {


            var client = new RestClient(URL_EXTERNAL_API);
            var request = new RestRequest("cadastro", Method.Post)
                .AddHeader("Authorization", $"Bearer {token}")
                .AddHeader(URL_EXTERNAL_API, URL_EXTERNAL_API)
                .AddJsonBody(model);

            var response = await client.PostAsync(request);

            return response;
        }

        public UserEntityModel ConvertUserEntityModelToDatabase(UserEntityModel model)
        {
            var newModel = new UserEntityModel
            {
                Nome = model.Nome,
                Email = model.Email,
                Data_Nascimento = model.Data_Nascimento,
                Data_Criacao = model.Data_Criacao
            };
            return newModel;
        }
        public static void CreateExcelTable(List<UserEntityModel> models)
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            var usersTableExcel = excel.Workbook.Worksheets.Add("Usuarios");
            usersTableExcel.TabColor = System.Drawing.Color.Black;
            usersTableExcel.DefaultRowHeight = 12;

            usersTableExcel.Row(1).Height = 20;
            usersTableExcel.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            usersTableExcel.Row(1).Style.Font.Bold = true;

            usersTableExcel.Cells[1, 1].Value = "ID";
            usersTableExcel.Cells[1, 2].Value = "Nome";
            usersTableExcel.Cells[1, 3].Value = "E-mail";
            usersTableExcel.Cells[1, 4].Value = "Data_Nascimento";
            usersTableExcel.Cells[1, 5].Value = "Data_Criacao";

            int index = 2;
            foreach (var item in models)
            {
                usersTableExcel.Cells[index, 1].Value = item.Codigo.ToString();
                usersTableExcel.Cells[index, 2].Value = item.Nome.ToString();
                usersTableExcel.Cells[index, 3].Value = item.Email.ToString();
                usersTableExcel.Cells[index, 4].Value = item.Data_Nascimento.ToString();
                usersTableExcel.Cells[index, 5].Value = item.Data_Criacao.ToString();
                index++;
            }
            usersTableExcel.Column(1).AutoFit();
            usersTableExcel.Column(2).AutoFit();
            usersTableExcel.Column(3).AutoFit();
            usersTableExcel.Column(4).AutoFit();
            usersTableExcel.Column(5).AutoFit();

            if (File.Exists(excelFilePath))
                File.Delete(excelFilePath);

            FileStream objFileStream = File.Create(excelFilePath);
            objFileStream.Close();

            File.WriteAllBytes(excelFilePath, excel.GetAsByteArray());

            excel.Dispose();
        }

       
    }
}