using Flow.Core.DomainModels;
using Flow.Core.Mediate.UserQuery;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace FlowUI.Components
{
    public partial class PostCardComponent
    {
        [Parameter]
        public Post Post { get; set; }

        public User PostOwner { get; set; }

        [Inject]
        protected IMediator _mediator { get; set; }
        [CascadingParameter]
        protected Task<AuthenticationState> _authenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            PostOwner = await _mediator.Send(new GetUserByIdRequest { UserId = Post.PostOwnerId });
        }
    }
}
