using Flow.Core.Contracts;
using Flow.Core.DomainModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Flow.Core.Mediate.GetFriendPosts
{
    public class GetFriendPostsRequestHandler : IRequestHandler<GetFriendPostsRequest, ICollection<Post>>
    {
        private readonly IPostRepository _repository;

        public GetFriendPostsRequestHandler(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Post>> Handle(GetFriendPostsRequest request, CancellationToken cancellationToken)
        {
            // TODO: More graceful and granular error handling
            try
            {
                return await _repository.GetPostsDescendingOrderByDateFromListOfIds(request.UserIds, request.Skip, request.Take);
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
