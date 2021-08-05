using Flow.Core.DomainModels;
using Flow.Core.Mediate.AddFriendRelation;
using Flow.Core.Mediate.UserQuery;
using FlowUI.Utilities.LoggedInUserRequest;
using MediatR;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace FlowUI.Pages
{
    public partial class ProfileDetails
    {
        [Inject]
        private IMediator _mediator { get; set; }

        [Parameter]
        public string UserId { get; set; }

        public User User { get; set; }

        public Guid LoggedInUserId { get; set; }

        public bool IsLoggedInUser
        { 
            get => LoggedInUserId == User.UserId; 
        }


        public ProfileDetails() {}

        protected override async Task OnInitializedAsync()
        {
            Guid guidId = Guid.Parse(UserId);
            User = await _mediator.Send(new GetUserByIdRequest { UserId = guidId });
            LoggedInUserId = await _mediator.Send(new GetIdLoggedInUserRequest());
        }

        protected async Task AddFriendButtonPressed()
        {
            await _mediator.Send(new AddFriendRelationRequest(LoggedInUserId, User.UserId));
        }
    }
}
