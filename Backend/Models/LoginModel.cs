namespace Backend.Models
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string? UserType { get; set; }
    }
}
