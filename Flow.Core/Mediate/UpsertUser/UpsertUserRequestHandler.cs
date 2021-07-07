using Flow.Core.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flow.Core.Mediate.UpsertUser
{
    public class UpsertUserRequestHandler : IRequestHandler<UpsertUserRequest>
    {
        private readonly IUserRepository _repository;

        public UpsertUserRequestHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpsertUserRequest request, CancellationToken cancellationToken)
        {
            await _repository.UpsertUserAsync(request.User);
            return Unit.Value;
        }
    }
}
