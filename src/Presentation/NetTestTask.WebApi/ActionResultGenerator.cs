using System.Net;
using Microsoft.AspNetCore.Mvc;
using NetTestTask.Domain.Dtos.Response;

namespace NetTestTask.WebApi
{
    public static class ActionResultGenerator
    {
        public static IActionResult CreateHttpResponseMessage(HttpStatusCode httpStatusCode, object content)
        {
            ServiceResponse<object> serviceResponse = new ServiceResponse<object> { IsSuccess = false };
            serviceResponse.Exception = content;
            var response = new ObjectResult(serviceResponse);
            response.StatusCode = (int)httpStatusCode;

            return response;
        }
    }
}
