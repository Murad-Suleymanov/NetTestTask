using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using NetTestTask.Common.Constants;
using NetTestTask.Common.CustomExceptions;
using NetTestTask.Common.ResourceValues;

namespace NetTestTask.WebApi.Filters
{
    public class UnhandledExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string messageCode = SystemNotificationCode.CommonErrorMessage;
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            string[] data = null;
            if (context.Exception.GetType().IsSubclassOf(typeof(CustomBaseException)))
            {
                var exception = (CustomBaseException)context.Exception;
                httpStatusCode = (HttpStatusCode)exception.HttpStatusCode;
                messageCode = exception.MessageCode;
                data = exception.Params;
                context.Result = ActionResultGenerator.CreateHttpResponseMessage(httpStatusCode, new { Message = data, Key = exception.MessageCode });
            }
            else
            {
                //context.Exception.Log();
                string message = string.Empty;

                message = context.Exception.Message;

                while (context.Exception.InnerException != null)
                {
                    context.Exception = context.Exception.InnerException;
                    message = context.Exception.Message;
                }

                context.Result = ActionResultGenerator.CreateHttpResponseMessage(httpStatusCode, new { HiddenMessage = message, Message = ErrorMessageValues.InternalServerError });
            }
        }
    }
}
