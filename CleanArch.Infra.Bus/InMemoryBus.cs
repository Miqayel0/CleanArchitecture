using CleanArch.Domain.Core.Bus;
using CleanArch.Domain.Core.Commands;
using CleanArch.Domain.Core.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infra.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<T> Send<T>(Message<T> message)
        {
            return _mediator.Send(message);
        }
    }
}
