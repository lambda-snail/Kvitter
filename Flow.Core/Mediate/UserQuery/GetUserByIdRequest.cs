using Flow.Core.DomainModels;
using MediatR;
using System;

namespace Flow.Core.Mediate.UserQuery
{
    public class GetUserByIdRequest : IRequest<User>
    {
        public Guid UserId { get; set; }
    }
}
