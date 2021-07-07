using MongoDB.Driver;
using Kvitter.Core.DomainModels;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Kvitter.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _usersDb;

        public UserRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("KvitterDb");
            _usersDb = database.GetCollection<User>("Users");
        }

        // Create and update with same method?
        public async Task CreateUser(User newUser)
        {

        }

        public async Task<User> GetUserById(Guid userId)
        {
            return null;
        }

        public async Task<ICollection<User>> FindUsersAsync(Func<User, bool> predicate)
        {
            return null;
        }

        public async Task DeleteUser(Guid userId)
        {

        }
    }
}
