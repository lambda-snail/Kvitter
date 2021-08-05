using Flow.Core.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Flow.Core.Mediate.DatabaseSize
{
    public class EstimatePostCountRequestHandler : IRequestHandler<EstimatePostCountRequest, long>
    {
        private readonly IPostRepository _repository;

        public EstimatePostCountRequestHandler(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<long> Handle(EstimatePostCountRequest request, CancellationToken cancellationToken)
        {
            if (request.UserIds?.Count >= 0)
            {
                return await _repository.GetEstimatedNumberOfPosts(request.UserIds);
            }
            else
            {
                return await _repository.GetEstimatedNumberOfPosts();
            }
        }
    }
}
