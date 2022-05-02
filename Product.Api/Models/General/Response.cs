using Newtonsoft.Json;

namespace Product.Api.Models.General
{
    public class Response
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }


        public Response()
        {

        }

        public Response(bool success, object data = null, string message = null)
        {
            Success = success;
            Data = data;
            Message = message;
        }

    }
}
