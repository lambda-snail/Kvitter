using MediatR;
using System;

namespace FlowUI.Utilities.LoggedInUserRequest
{
    /**
     * A request for the Id of the currently logged in user. This is intended to be used when only the id is needed,
     * which will save one trip to the database.
     */
    public class GetIdLoggedInUserRequest : IRequest<Guid> {}
}
