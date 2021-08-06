using Flow.Core.Contracts;
using Flow.Core.DomainModels;
using Flow.Core.Mediate.AddFriendRelation;
using Flow.Core.Mediate.UserQuery;
using MediatR;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FlowUI.Pages
{
    public partial class ProfileDetails
    {
        [Inject]
        private IMediator _mediator { get; set; }

        [Inject]
        private ILoggedInUserService _loggedInUserService { get; set; }

        [Parameter]
        public string UserId { get; set; }

        public User PageOwnerUser { get; set; }

        public User LoggedInUser { get; set; } = new User();

        public Guid LoggedInUserId { get; set; }

        public bool IsLoggedInUser
        { 
            get => LoggedInUserId == PageOwnerUser.UserId; 
        }


        public ProfileDetails() {}

        protected override async Task OnInitializedAsync()
        {
            Guid guidId = Guid.Parse(UserId);
            PageOwnerUser = await _mediator.Send(new GetUserByIdRequest { UserId = guidId });
            LoggedInUserId = await _loggedInUserService.GetLoggedInUserId();
            LoggedInUser = await _loggedInUserService.GetLoggedInUser();
        }

        protected async Task AddFriendButtonPressed()
        {
            await _mediator.Send(new AddFriendRelationRequest(LoggedInUserId, PageOwnerUser.UserId));
        }
    }
}
