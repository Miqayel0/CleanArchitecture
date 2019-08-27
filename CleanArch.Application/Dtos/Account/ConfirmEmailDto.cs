using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Application.Dtos.Account
{
    public class ConfirmEmailDto
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}
