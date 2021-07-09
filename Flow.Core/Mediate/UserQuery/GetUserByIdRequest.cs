using MediatR;
using System;

namespace Flow.Core.Mediate.UserQuery
{
    public class GetUserByIdRequest<User> : IRequest<User>
    {
        public Guid UserId { get; set; }
    }
}
