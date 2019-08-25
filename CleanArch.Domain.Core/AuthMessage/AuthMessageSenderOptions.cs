using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Domain.Core.AuthMessage
{
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}
