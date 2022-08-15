using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Api_Authentication.ExternalErrors;
using Web_Api_Authentication.ViewModels;

namespace Web_Api_Authentication.Validation
{
    public static class PostValidationModel
    {
        public static ErrorMessagesExternalApi ValidationPostModel(UserModel model, string token)
        {
            if (string.IsNullOrEmpty(model.Nome))
                return new ErrorMessagesExternalApi(10, new ErrorMessageDetails("Nome nulo", "O campo Nome não pode ser nulo!"));

            if (string.IsNullOrEmpty(model.Email))
                return new ErrorMessagesExternalApi(11, new ErrorMessageDetails("Email nulo", "O campo Email não pode ser nulo!"));

            return null!;
        }
        public static ErrorMessagesExternalApi ValidationToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return new ErrorMessagesExternalApi(12, new ErrorMessageDetails("Token nulo", "O campo Token não pode ser nulo!"));
            return null!;
        }
    }
}