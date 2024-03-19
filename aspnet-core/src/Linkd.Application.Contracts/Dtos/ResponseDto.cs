namespace Linkd.Dtos
{
    public class ResponseDto
    {
        public int Status { get; set; }
        public object? Data { get; set; }
        public string Message { get; set; }
        public string? Error { get; set; }
    }
}
