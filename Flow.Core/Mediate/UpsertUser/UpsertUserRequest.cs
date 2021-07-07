using Flow.Core.DomainModels;
using MediatR;

namespace Flow.Core.Mediate.UpsertUser
{
    public class UpsertUserRequest : IRequest
    {
        public User User { get; set; }
    }
}
