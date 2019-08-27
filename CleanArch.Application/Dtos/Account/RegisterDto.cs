using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Application.Dtos.Account
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string CallbackUrl { get; set; }
    }
}
