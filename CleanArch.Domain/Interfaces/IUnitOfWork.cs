using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task Complete(CancellationToken cancellationToken);
        void Complete();
    }
}
