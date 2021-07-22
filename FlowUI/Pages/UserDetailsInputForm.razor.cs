using AutoMapper;
using Flow.Core.DomainModels;
using Flow.Core.Mediate.UpsertUser;
using Flow.Core.Mediate.UserQuery;
using FlowUI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Security.Claims;
using System.Threading.Tasks;


namespace FlowUI.Pages
{
    public partial class UserDetailsInputForm
    {
        [Inject]
        public IMediator _mediator { get; set; }
        [Inject]
        public IMapper _mapper { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> authenticationStateTask { get; set; }

        private UserViewModel LoggedInUser { get; set; } = new UserViewModel();

        public UserDetailsInputForm() { }
        
        protected override async Task OnInitializedAsync()
        {
            AuthenticationState authenticationState = await authenticationStateTask;
            string userId = authenticationState.User.FindFirstValue(ClaimTypes.NameIdentifier);
            LoggedInUser = _mapper.Map<UserViewModel>(await _mediator.Send(new GetUserByIdRequest { UserId = Guid.Parse(userId) }));
        }

        protected async Task HandleValidSubmit()
        {
            await _mediator.Send(new UpsertUserRequest { User = _mapper.Map<User>(LoggedInUser) });
            _navigationManager.NavigateTo("/");
        }

        protected async Task HandleInvalidSubmit()
        {

        }

        //public string Image { get; set; }

        private async Task ProfilePictureSelected(InputFileChangeEventArgs evt)
        {
            var i = await evt.File.RequestImageFileAsync("jpg", 250, 250);

            var x = i.OpenReadStream();
            byte[] img = new byte[x.Length];
            await x.ReadAsync(img, 0, img.Length);

            LoggedInUser.ProfilePicture = img;

            //Image = "data:image/png;base64," + Convert.ToBase64String(img, 0, img.Length);
        }
    }
}
