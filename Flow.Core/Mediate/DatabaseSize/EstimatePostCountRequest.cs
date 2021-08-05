using MediatR;
using System;
using System.Collections.Generic;

namespace Flow.Core.Mediate.DatabaseSize
{
    public class EstimatePostCountRequest : IRequest<long>
    {
        public ICollection<Guid> UserIds { get; set; }
    }
}
