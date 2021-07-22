using MongoDB.Driver;
using Flow.Core.DomainModels;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Flow.Core.Contracts;
using MongoDB.Bson;
using System.Linq.Expressions;

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
            await _usersDb.ReplaceOneAsync(
                u => u.UserId == user.UserId,
                user,
                new ReplaceOptions { IsUpsert = true }
            );
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await _usersDb.Find(user => user.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<User>> FindUsersAsync(Expression<Func<User, bool>> predicate)
        {
            var filter = Builders<User>.Filter.Where(predicate);
            return await _usersDb.Find(filter).ToListAsync();
        }

        public async Task DeleteUserAsync(Guid userId)
        {

        }

        public async Task<long> GetEstimatedNumberOfUsers()
        {
            return await _usersDb.EstimatedDocumentCountAsync();
        }
    }
}
