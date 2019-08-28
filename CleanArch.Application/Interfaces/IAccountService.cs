using CleanArch.Application.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterOutDto> Register(RegisterDto input);
        Task ConfirmEmail(ConfirmEmailDto input);
        Task<AccountDto> Login(LoginDto input);
        Task<AccountDto> GetAccount(string input);
    }
}
