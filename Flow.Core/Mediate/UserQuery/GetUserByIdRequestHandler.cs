using Flow.Core.Contracts;
using Flow.Core.DomainModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Flow.Core.Mediate.UserQuery
{
    public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IFriendRelationRepository _friendRepository;

        public GetUserByIdRequestHandler(IUserRepository userRepository, IFriendRelationRepository friendRepository)
        {
            _userRepository = userRepository;
            _friendRepository = friendRepository;
        }
        public async Task<User> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserByIdAsync(request.UserId);

            if(user == null)
            {
                return null;
            }

            // if(request.IncludeFriends)

            IEnumerable<FriendRelation> friendRelations = await _friendRepository.GetByUserId(user.UserId);
            List<FriendModel> friendList = new();

            //TODO: Handle exceptions more gracefully
            try
            {
                foreach (var friendRelation in friendRelations)
                {
                    // TODO: Optimize so that only needed info is retrieved from DB
                    Guid friendId = (friendRelation.Users[0] == user.UserId) ? friendRelation.Users[1] : friendRelation.Users[0];
                    User friend = await _userRepository.GetUserByIdAsync(friendId);

                    FriendModel friendModel =
                        new FriendModel
                        {
                            FriendRelationId = friendRelation.Id,
                            UserId = friendId,
                            FirstName = friend.FirstName,
                            ShortDescription = friend.ShortDescription,
                            WhenConnected = friendRelation.ConnectedDate
                        };

                    friendList.Add(friendModel);
                }
            }
            catch(Exception e)
            {
                return null;
            }

            user.Friends = friendList;

            return user;
        }
    }
}
