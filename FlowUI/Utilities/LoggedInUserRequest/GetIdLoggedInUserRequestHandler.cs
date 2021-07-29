using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace FlowUI.Utilities.LoggedInUserRequest
{
    public class GetIdLoggedInUserRequestHandler : IRequestHandler<GetIdLoggedInUserRequest, Guid>
    {
        private readonly AuthenticationStateProvider _AuthenticationStateProvider;

        public GetIdLoggedInUserRequestHandler(AuthenticationStateProvider authenticationStateProvider)
        {
            _AuthenticationStateProvider = authenticationStateProvider;
        }

        public async Task<Guid> Handle(GetIdLoggedInUserRequest request, CancellationToken cancellationToken)
        {
            AuthenticationState authenticationState = await _AuthenticationStateProvider.GetAuthenticationStateAsync();
            string userId = authenticationState.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(string.IsNullOrWhiteSpace(userId))
            {
                return Guid.Empty;
            }
            else
            {
                return Guid.Parse(userId);
            }
        }
    }
}
