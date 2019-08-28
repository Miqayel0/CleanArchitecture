using CleanArch.Application.Dtos.Auth;

namespace CleanArch.Application.Dtos.Account
{
    public class AccountDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public AccessToken AccessToken { get; set; }
    }
}