namespace NetTestTask.Domain.Dtos.Response
{
    public class ServiceResponse<T> where T : class
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public dynamic Exception { get; set; }
    }
}
