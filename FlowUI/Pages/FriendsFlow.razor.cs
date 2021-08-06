using Flow.Core.Contracts;
using Flow.Core.DomainModels;
using Flow.Core.Mediate.DatabaseSize;
using Flow.Core.Mediate.GetFriendPosts;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowUI.Pages
{
    public partial class FriendsFlow
    {
        [Inject]
        protected IMediator _mediator { get; set; }

        [Inject]
        private ILoggedInUserService _loggedInUserService { get; set; }

        public User LoggedInUser { get; set; }
        public Virtualize<Post> PostVirtualizer { get; set; }

        public FriendsFlow() {}

        protected override async Task OnInitializedAsync()
        {
            LoggedInUser = await _loggedInUserService.GetLoggedInUser();
        }

        public async ValueTask<ItemsProviderResult<Post>> LoadPostsPaged(ItemsProviderRequest request)
        {
            List<Guid> userIds = new();
            foreach(var friend in LoggedInUser.Friends)
            {
                userIds.Add(friend.UserId);
            }

            ICollection<Post> posts = await _mediator.Send(new GetFriendPostsRequest(userIds, request.StartIndex, request.Count));
            var totalPostCount = await _mediator.Send(new EstimatePostCountRequest { UserIds = userIds }); // TODO: Should get number of friends posts here
            return new ItemsProviderResult<Post>(posts, (int)totalPostCount); // This might need tweaking if the application grows
        }
    }
}
