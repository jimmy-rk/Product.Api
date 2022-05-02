using Microsoft.AspNetCore.Mvc;

namespace Product.Api.Models.General
{
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string ResponseMessage { get; set; }
        public int StatusCode { get; set; }
        public ServiceResponse(ServiceResponse serviceResponse)
            : this(serviceResponse.Success, serviceResponse.ResponseMessage, serviceResponse.StatusCode)
        {
        }

        public ServiceResponse(bool success, int statusCode)
        {
            Success = success;
            StatusCode = statusCode;
        }

        public ServiceResponse(bool success, string responseMessage, int statusCode)
        {
            Success = success;
            ResponseMessage = responseMessage;
            StatusCode = statusCode;
        }

        public ObjectResult ConvertToObjectResult()
        {
            return new ObjectResult(new Response(Success, message: ResponseMessage))
            {
                StatusCode = StatusCode
            };
        }
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public T Data { get; set; }
        public ServiceResponse(ServiceResponse serviceResponse)
            : base(serviceResponse)
        {
        }
        public ServiceResponse(bool success, string responseMessage, int statusCode)
            : base(success, responseMessage, statusCode)
        {
        }
        public ServiceResponse(bool success, T data, int statusCode)
            : this(success, data, null, statusCode)
        {
        }
        public ServiceResponse(bool success, T data, string responseMessage, int statusCode)
            : this(success, responseMessage, statusCode)
        {
            Data = data;
        }

        public  ObjectResult ConvertToObjectResult()
        {
            return new ObjectResult(new Response(Success, data: Data, message: ResponseMessage))
            {
                StatusCode = this.StatusCode
            };
        }
    }

}
