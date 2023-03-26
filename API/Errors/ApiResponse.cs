using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int StatusCode, string Message = null )
        {
            this.Message = Message?? GetDefaultMessageForStatusCode(StatusCode);
            this.StatusCode= StatusCode ;
        }

        public int StatusCode {get;set;}
        public string Message{get;set;}
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad Request, you have made",
                401 => "Authorized, You are not",
                404 => "Resource found, it was not",
                500 => "Error are the path to the dark side",
                _=> null
            };
        }
    }
}