using AutoMapper;
using Flow.Core.Mediate.UserQuery;
using FlowUI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace FlowUI.Pages
{
    public partial class ProfileDetails
    {
        [Inject]
        private IMapper _mapper { get; set; }
        [Inject]
        private IMediator _mediator { get; set; }

        [Parameter]
        public String UserId { get; set; }

        public UserViewModel User { get; set; }
        public bool IsLoggedInUser { get; set; } = false;


        public ProfileDetails() {}

        protected override async Task OnInitializedAsync()
        {
            Guid guidId = Guid.Parse(UserId);
            User = _mapper.Map<UserViewModel>(
                    await _mediator.Send(new GetUserByIdRequest { UserId = guidId })
                );
        }
    }
}
