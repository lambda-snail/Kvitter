using Flow.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flow.Core.Contracts
{
    public interface IPostRepository
    {
        /**
         * Retreive a list of all posts by a given user. Warning, can potentially consume a lot of resources.
         */
        Task<ICollection<Post>> GetPostByUserId(Guid userId);

        /**
         * Retreive a list of all posts by a given user. Use the skip and limit parameters to limit the query, saving resouces
         * by only fetching the posts that you need at the moment.
         * <param name="skip">The number of posts to skip.</param>
         * <param name="take">The number of posts to take after skipping skip posts.</param>
         */
        Task<ICollection<Post>> GetPostByUserId(Guid userId, int skip, int take);

        /**
         * Update an existing post or insert new post if the post does not exist.
         */
        Task UpsertPost(Post post);
    }
}
