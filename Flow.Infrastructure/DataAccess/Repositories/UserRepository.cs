using MongoDB.Driver;
using Flow.Core.DomainModels;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Flow.Core.Contracts;
using MongoDB.Bson;

namespace Flow.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _usersDb;

        public UserRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("FlowDb");
            _usersDb = database.GetCollection<User>("Users");
        }

        public async Task UpsertUserAsync(User user)
        {
            BsonBinaryData binGuid = new BsonBinaryData(user.UserId, GuidRepresentation.Standard);
            await _usersDb.ReplaceOneAsync(
                new BsonDocument("_id", binGuid),
                user,
                new ReplaceOptions { IsUpsert = true }
            );
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return null;
        }

        public async Task<ICollection<User>> FindUsersAsync(Func<User, bool> predicate)
        {
            return null;
        }

        public async Task DeleteUserAsync(Guid userId)
        {

        }
    }
}
