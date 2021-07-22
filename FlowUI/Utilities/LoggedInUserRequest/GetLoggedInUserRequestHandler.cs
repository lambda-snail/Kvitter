using AutoMapper;
using Flow.Core.Mediate.UserQuery;
using FlowUI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace FlowUI.Utilities.LoggedInUserRequest
{
    public class GetLoggedInUserRequestHandler : IRequestHandler<GetLoggedInUserRequest, UserViewModel>
    {
        private readonly AuthenticationStateProvider _AuthenticationStateProvider;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetLoggedInUserRequestHandler(AuthenticationStateProvider authenticationStateProvider, 
                                             IMediator mediator,
                                             IMapper mapper)
        {
            _AuthenticationStateProvider = authenticationStateProvider;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Handle(GetLoggedInUserRequest request, CancellationToken cancellationToken)
        {
            AuthenticationState authenticationState = await _AuthenticationStateProvider.GetAuthenticationStateAsync();
            string userId = authenticationState.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _mapper.Map<UserViewModel>(await _mediator.Send(new GetUserByIdRequest { UserId = Guid.Parse(userId) }));
        }
    }
}
