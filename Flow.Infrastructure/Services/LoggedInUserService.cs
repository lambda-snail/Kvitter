using Flow.Core.Contracts;
using Flow.Core.DomainModels;
using Flow.Core.Mediate.UserQuery;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Flow.Infrastructure.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IMediator _mediator;

        public LoggedInUserService(AuthenticationStateProvider authenticationStateProvider,
                                   IMediator mediator)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _mediator = mediator;
        }

        public async Task<User> GetLoggedInUser()
        {
            Guid userId = await GetLoggedInUserId();
            return await _mediator.Send(new GetUserByIdRequest { UserId = userId });
        }

        public async Task<Guid> GetLoggedInUserId()
        {
            AuthenticationState authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            string userId = authenticationState.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(userId);
        }
    }
}
