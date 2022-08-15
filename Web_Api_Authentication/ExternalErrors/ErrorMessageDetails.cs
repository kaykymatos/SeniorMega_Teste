using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web_Api_Authentication.ExternalErrors
{
    public class ErrorMessageDetails
    {
        public string Header { get; set; } = default!;
        public string Message { get; set; } = default!;
        public ErrorMessageDetails(string header, string message)
        {
            Header = header;
            Message = message;
        }
    }
}