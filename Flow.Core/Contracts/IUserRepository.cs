using Flow.Core.DomainModels;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Flow.Core.Contracts
{
    public interface IUserRepository
    {
        /** Insert or update a User into the database. */
        Task UpsertUserAsync(User newUser);

        /** Removes the user with the specified id, if it exists in the database. */
        Task DeleteUserAsync(Guid userId);

        /** Returns a list of users that fulfill the provided predicate. */
        Task<ICollection<User>> FindUsersAsync(Expression<Func<User, bool>> predicate);

        /** Retreive the suer with the specified id, or null if it does not exist. */
        Task<User> GetUserByIdAsync(Guid userId);

        /**
         * Retreive the number of Users in the database.
         */
        Task<long> GetEstimatedNumberOfUsers();
    }
}