using Flow.Core.Contracts;
using Flow.Core.DomainModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Flow.Core.Mediate.UserQuery
{
    public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest<User>, User>
    {
        private readonly IUserRepository _repository;

        public GetUserByIdRequestHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public Task<User> Handle(GetUserByIdRequest<User> request, CancellationToken cancellationToken)
        {
            return _repository.GetUserByIdAsync(request.UserId);
        }
    }
}
