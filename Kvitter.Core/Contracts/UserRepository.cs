using Kvitter.Core.DomainModels;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRepository
{
    Task CreateUser(User newUser);

    /** Removes the user with the specified id, if it exists in the database. */
    Task DeleteUser(Guid userId);

    /** Returns a list of users that fulfill the provided predicate. */
    Task<ICollection<User>> FindUsersAsync(Func<User, bool> predicate);

    /** Retreive the suer with the specified id, or null if it does not exist. */
    Task<User> GetUserById(Guid userId);
}