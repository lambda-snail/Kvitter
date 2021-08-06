using AutoMapper;
using Flow.Core.Contracts;
using Flow.Core.DomainModels;
using Flow.Core.Mediate.UpsertUser;
using FlowUI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Components;
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

        [Inject]
        private ILoggedInUserService _loggedInUserService { get; set; }

        private UserViewModel LoggedInUser { get; set; } = new UserViewModel();

        public UserDetailsInputForm() { }
        
        protected override async Task OnInitializedAsync()
        {
            LoggedInUser = _mapper.Map<UserViewModel>(await _loggedInUserService.GetLoggedInUser());
        }

        protected async Task HandleValidSubmit()
        {
            await _mediator.Send(new UpsertUserRequest { User = _mapper.Map<User>(LoggedInUser) });
            _navigationManager.NavigateTo("/");
        }

        protected async Task HandleInvalidSubmit()
        {

        }
    }
}
