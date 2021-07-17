using Flow.Core.Contracts;
using Flow.Core.DomainModels;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Flow.Core.Mediate.GetPosts
{
    public class GettAllPostsPagedRequestHandler : IRequestHandler<GettAllPostsPagedRequest, ICollection<Post>>
    {
        private readonly IPostRepository _repository;

        public GettAllPostsPagedRequestHandler(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Post>> Handle(GettAllPostsPagedRequest request, CancellationToken cancellationToken)
        {
            return await _repository.GetPosts(request.Skip, request.Take);
        }
    }
}
