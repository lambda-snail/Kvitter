using AutoMapper;
using Flow.Core.Contracts;
using Flow.Core.DomainModels;
using Flow.Core.Mediate.InsertPost;
using FlowUI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Components;
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

        [Inject]
        private ILoggedInUserService _loggedInUserService { get; set; }

        [Parameter]
        public EventCallback OnPostCreated { get; set; }

        protected override void OnInitialized()
        {
            ShowDialog = false;
        }

        public async Task HandleValidSubmit()
        {
            Post.PostedDateTime = System.DateTimeOffset.Now;
            Post.PostOwnerId = await _loggedInUserService.GetLoggedInUserId();

            await _mediator.Send( new InsertPostRequest { Post= _mapper.Map<Post>(Post) } );

            await OnPostCreated.InvokeAsync();

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
    }
}
