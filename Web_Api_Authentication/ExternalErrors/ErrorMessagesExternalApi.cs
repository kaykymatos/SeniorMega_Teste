using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web_Api_Authentication.ExternalErrors
{
    public class ErrorMessagesExternalApi
    {
        public int Cod { get; set; }
        public ErrorMessageDetails Error { get; set; } = default!;
        public ErrorMessagesExternalApi(int cod, ErrorMessageDetails error)
        {
            Cod = cod;
            Error = error;
        }
    }
}