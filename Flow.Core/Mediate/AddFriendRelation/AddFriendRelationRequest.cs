using Flow.Core.DomainModels;
using MediatR;
using System;

namespace Flow.Core.Mediate.AddFriendRelation
{
    public record AddFriendRelationRequest(Guid User1, Guid User2) : IRequest;
}
