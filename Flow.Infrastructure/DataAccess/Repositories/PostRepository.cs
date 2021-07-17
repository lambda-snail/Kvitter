using Flow.Core.Contracts;
using Flow.Core.DomainModels;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flow.Infrastructure.DataAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IMongoCollection<Post> _database;

        public PostRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("FlowDb");
            _database = database.GetCollection<Post>("Posts");
        }

        public async Task InsertPost(Post post)
        {
            if (post.PostId != Guid.Empty)
            {
                throw new ArgumentException("Error: Attempting to add a post with non-null or non-default Id. Did you mean to update instead?");
            }

            await _database.InsertOneAsync(post);
        }

        public async Task UpdatePost(Post post)
        {
            if (post.PostId == Guid.Empty)
            {
                throw new ArgumentException("Error: Attempting to update a post with null or default Id. Did you mean to insert instead?");
            }

            var filter = Builders<Post>.Filter.Where(p => p.PostId == post.PostId);
            var options = new FindOneAndReplaceOptions<Post, Post> { IsUpsert = true };
            await _database.FindOneAndReplaceAsync(filter, post, options);
        }

        public async Task<ICollection<Post>> GetPostsDescendingOrderByDate(int skip, int take)
        {
            if (skip >= 0 || take >= 0)
            {
                return await _database.Find(new BsonDocument())
                                       .Sort(Builders<Post>.Sort.Descending(post => post.PostedDateTime))
                                       .Skip(skip)
                                       .Limit(take)
                                       .ToListAsync();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Error: Attemtping to skip or retreive a negative number of posts.");
            }
        }

        public async Task<ICollection<Post>> GetPostByUserId(Guid userId)
        {
            return await GetPostByUserId(userId, 0, 0);
        }

        public async Task<ICollection<Post>> GetPostByUserId(Guid userId, int skip, int take)
        {
            if (skip >= 0 || take >= 0)
            {
                return await _database.Find(post => post.PostOwnerId == userId)
                                      .Skip(skip)
                                      .Limit(take)
                                      .ToListAsync();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Error: Attemtping to skip or retreive a negative number of posts.");
            }
        }

        public async Task<long> GetEstimatedNumberOfPosts()
        {
            return await _database.EstimatedDocumentCountAsync();
        }
    }
}
