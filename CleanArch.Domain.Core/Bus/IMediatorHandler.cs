using CleanArch.Domain.Core.Commands;
using CleanArch.Domain.Core.Events;
using System.Threading.Tasks;

namespace CleanArch.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task<TResponse> Send<TResponse>(Message<TResponse> massage);
    }
}
