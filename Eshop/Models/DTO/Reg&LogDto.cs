namespace Models.DTO
{
    public class LoginRequestModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class LoginResponseModel
    {
        public string? UserName { get; set; }
        public string? AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }

    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }

    public class AccountDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<OrderDTO> Orders { get; set; } = [];
    }
}