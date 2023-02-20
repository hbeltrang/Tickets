using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Tickets.Api.Errors
{
    public class CodeErrorResponse
    {
        [JsonProperty(PropertyName = "statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string[]? Message { get; set; }


        public CodeErrorResponse(int statusCode, string[]? message = null)
        {
            StatusCode = statusCode;
            if (message is null)
            {
                Message = new string[0];
                var text = GetDefaultMessageStatusCode(statusCode);
                Message[0] = text;
            }
            else
            {
                Message = message;
            }
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "The request has errors",
                401 => "Don´t have authorization for this resource",
                404 => "Resource not found",
                500 => "There are errors in the server",
                _ => string.Empty
            };

        }


    }
}
