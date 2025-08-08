using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Error
{
    public class ApiErrorResponse(int Statuscode, string message, string? details)
    {
        public int StatusCode { get; set; } = Statuscode;
        public string Message { get; set; } = message;
        public string? Details { get; set; } = details;


    }
}