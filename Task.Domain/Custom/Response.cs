using Task.Infra.Constants;

namespace Task.Domain.Custom
{
    public class Response
    {
        public dynamic Data { get; set; }
        public bool IsSuccessed { get; set; }
        public List<string> Errors { get; set; }
        public string Message { get; set; }
        public HttpResponseCustom HttpResponse { get; set; }
    }
}
