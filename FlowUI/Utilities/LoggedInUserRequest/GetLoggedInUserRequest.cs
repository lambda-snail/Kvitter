using FlowUI.ViewModels;
using MediatR;

namespace FlowUI.Utilities.LoggedInUserRequest
{
    public class GetLoggedInUserRequest : IRequest<UserViewModel>
    {
    }
}
