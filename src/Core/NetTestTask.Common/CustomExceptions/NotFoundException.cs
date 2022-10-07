using System.Runtime.Serialization;

namespace NetTestTask.Common.CustomExceptions
{
    [Serializable]
    public class NotFoundException : CustomBaseException
    {
        public NotFoundException(string messageCode, params string[] data)
        {
            MessageCode = messageCode;
            HttpStatusCode = 404;
            Params = data;
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }

        public override string MessageCode { get; set; }
        public override int HttpStatusCode { get; set; }
        public override string[] Params { get; set; }
    }
}
