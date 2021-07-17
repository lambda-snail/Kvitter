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
         * Retreive a list of all posts by any user beginning with the newest posts. Use the skip and limit parameters to
         * limit the query, saving resouces by only fetching the posts that you need at the moment.
         * <param name="skip">The number of posts to skip.</param>
         * <param name="take">The number of posts to take after skipping skip posts.</param>
         */
        Task<ICollection<Post>> GetPostsDescendingOrderByDate(int skip, int take);

        /**
         * Retreive a list of all posts by a given user. Use the skip and limit parameters to limit the query, saving resouces
         * by only fetching the posts that you need at the moment.
         * <param name="skip">The number of posts to skip.</param>
         * <param name="take">The number of posts to take after skipping skip posts.</param>
         */
        Task<ICollection<Post>> GetPostByUserId(Guid userId, int skip, int take);

        /**
         * Insert new post if the post does not exist.
         * <exception cref="ArgumentException">Thrown when the Id of the provided Post is not null or default.</exception>
         */
        Task InsertPost(Post post);

        /**
         * Update an existing post.
         * <exception cref="ArgumentException">Thrown when the Id of the provided Post is null or default.</exception>
         */
        Task UpdatePost(Post post);

        /**
         * Retreive the number of Posts in the database.
         */
        Task<long> GetEstimatedNumberOfPosts();
    }
}
