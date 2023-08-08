namespace EmployeeWebApi.Model.DTO
{
    public class ResponseDTO
    {
        public object? Result { get; set; }
        public Boolean ISSuccess { get; set; } = true;
        public string ErrorMessage { get; set; } = "";  
    }
}
