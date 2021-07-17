using Flow.Core.DomainModels;
using MediatR;
using System.Collections.Generic;

namespace Flow.Core.Mediate.GetPosts
{
    public class GettAllPostsPagedRequest : IRequest<ICollection<Post>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
