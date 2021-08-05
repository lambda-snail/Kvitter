using Flow.Core.DomainModels;
using MediatR;
using System;
using System.Collections.Generic;

namespace Flow.Core.Mediate.GetFriendPosts
{
    public record GetFriendPostsRequest(IEnumerable<Guid> UserIds, int Skip, int Take) : IRequest<ICollection<Post>>;
}
