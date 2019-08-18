using CleanArch.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Domain.Core.Queries
{
    public abstract class Query<TRespnse> : Message<TRespnse>
    { 
    }
}
