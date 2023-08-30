namespace Application.Wrappers
{
    public class Response<T> //Generic class response
    {
        public Response() { }
        public Response(T data,string message=null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; } 
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
