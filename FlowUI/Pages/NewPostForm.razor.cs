using AutoMapper;
using Flow.Core.DomainModels;
using Flow.Core.Mediate.InsertPost;
using Flow.Core.Mediate.UserQuery;
using FlowUI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlowUI.Pages
{
    public partial class NewPostForm
    {
        public NewPostViewModel Post { get; set; } = new NewPostViewModel();

        public bool ShowDialog { get; set; } = false;

        [Inject]
        private  IMediator _mediator { get; set; }

        [Inject]
        private IMapper _mapper { get; set; }
        
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        protected override void OnInitialized()
        {
            ShowDialog = false;
        }

        public async Task HandleValidSubmit()
        {
            Post.PostedDateTime = System.DateTimeOffset.Now;
            Post.PostOwnerId = (await GetLoggedInUserAsync()).UserId;

            Post x = _mapper.Map<Post>(Post);

            await _mediator.Send( new InsertPostRequest { Post= _mapper.Map<Post>(Post) } );

            CloseDialog();
        }

        public void HandleInvalidSubmit()
        {

        }

        public void OpenDialog()
        {
            if(!ShowDialog)
            {
                ResetDialog();
                ShowDialog = true;
                StateHasChanged();
            }
        }

        public void CloseDialog()
        {
            if(ShowDialog)
            {
                ResetDialog();
                ShowDialog = false;
                StateHasChanged();
            }
        }

        private void ResetDialog()
        {
            Post = new NewPostViewModel { Title = string.Empty, Content = string.Empty };
        }

        // TODO: Violates DRY, refactor to promote code reuse.
        protected async Task<User> GetLoggedInUserAsync()
        {
            AuthenticationState authenticationState = await authenticationStateTask;
            string userId = authenticationState.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _mediator.Send(new GetUserByIdRequest { UserId = Guid.Parse(userId) });
        }
    }
}
