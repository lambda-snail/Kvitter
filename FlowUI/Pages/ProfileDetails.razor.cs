using Flow.Core.DomainModels;
using Flow.Core.Mediate.AddFriendRelation;
using Flow.Core.Mediate.UserQuery;
using FlowUI.Utilities.LoggedInUserRequest;
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
            LoggedInUserId = await _mediator.Send(new GetIdLoggedInUserRequest());
            LoggedInUser = await _mediator.Send( new GetUserByIdRequest { UserId = LoggedInUserId } );
        }

        protected async Task AddFriendButtonPressed()
        {
            await _mediator.Send(new AddFriendRelationRequest(LoggedInUserId, PageOwnerUser.UserId));
        }
    }
}
