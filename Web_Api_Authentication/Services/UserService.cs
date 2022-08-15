using System.Data;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestSharp;
using RestSharp.Authenticators;
using Web_Api_Authentication.ExternalErrors;
using Web_Api_Authentication.Interfaces.Repository;
using Web_Api_Authentication.Interfaces.Services;
using Web_Api_Authentication.Models;
using Web_Api_Authentication.Validation;
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
        public async Task<RestResponse> GetAllUsers(string token)
        {
            RestResponse tokenError = new RestResponse();
            ErrorMessagesExternalApi? valicatioToken = PostValidationModel.ValidationToken(token);

            if (valicatioToken != null)
            {
                tokenError.Content = "Token Não pode ser nulo";
                return tokenError;
            }

            RestClient client = new RestClient(URL_EXTERNAL_API);
            RestRequest request = new RestRequest("cadastro/", Method.Get)
            .AddHeader("Authorization", $"Bearer {token}");
            RestResponse restResponse = await client.GetAsync(request);

            if (IsResponseAnErrorMessage(restResponse.Content!))
            {
                List<UserEntityModel>? response = await client.GetAsync<List<UserEntityModel>>(request);
                CreateExcelTable(response!);
                foreach (UserEntityModel item in response!)
                {
                    UserEntityModel? itemConvert = ConvertUserEntityModelToDatabase(item);
                    _repository.PostUser(itemConvert);
                }
            }
            return restResponse;

        }
        public bool IsResponseAnErrorMessage(string response)
        {
            try
            {
                ErrorMessagesExternalApi? descerializeErrorHeader = JsonSerializer.Deserialize<ErrorMessagesExternalApi>(response);
                if (descerializeErrorHeader?.Error == null)
                    return true;

                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public async Task<RestResponse> GetToken(LoginModel model)
        {
            RestClient client = new RestClient(URL_EXTERNAL_API);
            client.Authenticator = new HttpBasicAuthenticator(model.UserName, model.Password);
            RestRequest request = new RestRequest("get-token/", Method.Get);

            RestResponse response = await client.ExecuteAsync(request);

            return response;
        }

        public async Task<RestResponse> GetUserByCode(long codigo, string token)
        {
            RestClient client = new RestClient(URL_EXTERNAL_API);
            RestRequest request = new RestRequest($"cadastro/{codigo}", Method.Get)
            .AddHeader("Authorization", $"Bearer {token}");

            RestResponse response = await client.ExecuteAsync(request);

            return response;

        }
        public object PostUser(string token, UserModel model)
        {
            ErrorMessagesExternalApi? validationModel = PostValidationModel.ValidationPostModel(model, token);
            ErrorMessagesExternalApi? validationToken = PostValidationModel.ValidationToken(token);
            if (validationModel != null)
                return validationModel;

            if (validationToken != null)
                return validationToken;

            string urlRequest = string.Format($"{URL_EXTERNAL_API}/cadastro");
            WebRequest requestObject = WebRequest.Create(urlRequest);
            requestObject.Headers.Add("Authorization", $"Bearer {token}");
            requestObject.Method = "POST";
            requestObject.ContentType = "application/json";

            var newObject = ConvertUserModelToPostApiModel(model);
            string dataObject = JsonSerializer.Serialize(newObject);

            using (var streWriter = new StreamWriter(requestObject.GetRequestStream()))
            {
                streWriter.Write(dataObject);
                streWriter.Flush();
                streWriter.Close();


                var responseRequestStream = (HttpWebResponse)requestObject.GetResponse();

                using (var streReader = new StreamReader(responseRequestStream.GetResponseStream()))
                {
                    var reader = streReader.ReadToEnd();
                    return reader;
                }
            }
        }

        public UserEntityModel ConvertUserEntityModelToDatabase(UserEntityModel model)
        {
            UserEntityModel? newModel = new UserEntityModel
            {
                Nome = model.Nome,
                Email = model.Email,
                Data_Nascimento = model.Data_Nascimento,
                Data_Criacao = model.Data_Criacao
            };
            return newModel;
        }
        public PostModel ConvertUserModelToPostApiModel(UserModel model)
        {
            PostModel? postModel = new PostModel
            {
                Nome = model.Nome,
                Email = model.Email,
                Data_Nascimento = model.Data_Nascimento.ToString("yyyy-MM-dd"),
                Data_Criacao = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            return postModel;
        }
        public static void CreateExcelTable(List<UserEntityModel> models)
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            ExcelWorksheet? usersTableExcel = excel.Workbook.Worksheets.Add("Usuarios");
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
            foreach (UserEntityModel item in models)
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