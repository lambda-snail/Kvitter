using Flow.Core.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Flow.Core.Mediate.InsertPost
{
    public class InsertPostRequestHandler : IRequestHandler<InsertPostRequest>
    {
        private readonly IPostRepository _repository;

        public InsertPostRequestHandler(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(InsertPostRequest request, CancellationToken cancellationToken)
        {
            await _repository.InsertPost(request.Post);
            return Unit.Value;
        }
    }
}
