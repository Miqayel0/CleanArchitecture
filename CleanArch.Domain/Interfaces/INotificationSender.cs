using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Interfaces
{
    public interface INotificationSender
    {
        Task SendAsync(string subject, string body, string from);
    }
}
