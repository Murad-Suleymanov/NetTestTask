using NetTestTask.Common.Constants;
using System.Runtime.Serialization;

namespace NetTestTask.Common.CustomExceptions
{
    [Serializable]
    public class UnauthorizedException : CustomBaseException
    {
        public UnauthorizedException(string messageCode = SystemNotificationCode.Unauthorized, params string[] data)
        {
            MessageCode = messageCode;
            HttpStatusCode = 401;
            Params = data;
        }

        protected UnauthorizedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override string MessageCode { get; set; }
        public override int HttpStatusCode { get; set; }
        public override string[] Params { get; set; }
    }
}
