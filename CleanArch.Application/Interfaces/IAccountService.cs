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
        Task Login(LoginDto input);
    }
}
