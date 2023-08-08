namespace FrontEnd.Models
{
    public class ResponseDTO
    {
        public object? Result { get; set; }
        public Boolean IsSuccess { get; set; } = true;
        public string ErrorMessage { get; set; } = "";
    }
}
