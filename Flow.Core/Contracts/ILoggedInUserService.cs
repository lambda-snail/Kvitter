using Flow.Core.DomainModels;
using System;
using System.Threading.Tasks;

namespace Flow.Core.Contracts
{
    /**
     * A simple helper to abstract away the operation of retrieving the logged in user.
     */
    public interface ILoggedInUserService
    {
        Task<Guid> GetLoggedInUserId();
        Task<User> GetLoggedInUser();

        //bool IsLoggedIn();
    }
}
