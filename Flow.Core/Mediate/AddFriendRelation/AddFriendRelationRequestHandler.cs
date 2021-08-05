using Flow.Core.Contracts;
using Flow.Core.DomainModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flow.Core.Mediate.AddFriendRelation
{
    class AddFriendRelationRequestHandler : IRequestHandler<AddFriendRelationRequest>
    {
        private readonly IFriendRelationRepository _repository;

        public AddFriendRelationRequestHandler(IFriendRelationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddFriendRelationRequest request, CancellationToken cancellationToken)
        {
            var friendRelation = new FriendRelation();
            friendRelation.Users = new(2);

            friendRelation.Users.Add(request.User1);
            friendRelation.Users.Add(request.User2);
            friendRelation.ConnectedDate = DateTimeOffset.Now;

            await _repository.AddFriendRelationAsync(friendRelation);

            return Unit.Value;
        }
    }
}
