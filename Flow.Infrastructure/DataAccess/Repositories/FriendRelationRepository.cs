using Flow.Core.Contracts;
using Flow.Core.DomainModels;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flow.Infrastructure.DataAccess.Repositories
{
    /**
     * A friendship is stored in the DB as a pair  of user id's, where the order doesn't matter.
     * 1. In the first iteration, friend requests are assumed to be accepted immediately.
     * 2. In the second iteration, freind requests will need to be accepted first, and the Add method will
     * require two distinct ids to work.
     */
    public class FriendRelationRepository : IFriendRelationRepository
    {
        private readonly IMongoCollection<FriendRelation> _database;

        public FriendRelationRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("FlowDb");
            _database = database.GetCollection<FriendRelation>("Connections");
        }

        public async Task AddFriendRelationAsync(FriendRelation r)
        {
            await _database.InsertOneAsync(r);
        }

        public async Task<IEnumerable<FriendRelation>> GetByUserId(Guid userId)
        {
            //FilterDefinition<FriendRelation> filter =
            //    Builders<FriendRelation>.Filter.ElemMatch<Guid>(r => r.Users, id => id == userId);
            //return _database.Find(filter).ToListAsync();

            // db.Connections.find({ "Users": { $elemMatch: { $eq: new BinData(3, 'RoR7hceFl0Sb8QN5ATlQGg==') } }}, { "_id":1 })
            var f = new BsonDocument
            {
                {
                    "Users", new BsonDocument {{
                                "$elemMatch", new BsonDocument {{ "$eq", BsonBinaryData.Create(userId) }}
                    }}
                }
            };

            var x = _database.Find<FriendRelation>(f);
            var y = await x.ToListAsync();

            return await _database.Find(f).ToListAsync();
        }

        public async Task<long> GetEstimatedCollectionSize()
        {
            return await _database.EstimatedDocumentCountAsync();
        }
    }
}
