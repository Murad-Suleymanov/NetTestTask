using System.Runtime.Serialization;

namespace NetTestTask.Common.CustomExceptions
{
    [Serializable]
    public class BadRequestException : CustomBaseException
    {
        public BadRequestException(string messageCode, params string[] data)
        {
            MessageCode = messageCode;
            HttpStatusCode = 400;
            Params = data;
        }

        protected BadRequestException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }

        public override string MessageCode { get; set; }
        public override int HttpStatusCode { get; set; }
        public override string[] Params { get; set; }
    }
}
