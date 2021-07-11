using System;
using System.Collections.Generic;

namespace Flow.Core.DomainModels
{
    /** 
     * A class that represents a post by a user. The post has a reference to the owner as well as a list of comments embedded.
     */
    public class Post
    {
        public Guid PostId { get; set; }
        public Guid PostOwnerId { get; set; }
        public DateTimeOffset PostedDateTime { get; set; }
        public string Content { get; set; }
        public List<Comment> Comments { get; set; }

        /** A list of ids of all users who have liked the post. */
        //public List<Guid> Likes { get; set; }

        /**
         * Add a comment to the post. While comments can be added using a reference to the Comments list, this method
         * includes validation of the comment, so it is the preferred way of adding comments to the user.
         * <exception cref="ArgumentException">Thrown when the contents of the comment is empty or null.</exception>
         */
        public void AddComment(Comment comment)
        {
            if(string.IsNullOrWhiteSpace(comment.Content))
            {
                throw new ArgumentException("Contents of comment cannot be null or empty.");
            }

            Comments.Add(comment);
        }

        /**
         * Add a new user to the list of likes. While likes can be added using a reference to the Likes list,
         * this method includes validation of the id, so it is the preferred way of adding likes to the user.
         * <exception cref="ArgumentException">Thrown if id is null.</exception>
         * <exception cref="InvalidOperationException">Thrown when user has already liked the post.</exception>
         *
        public void Like(User user)
        {
            if(user.UserId == null)
            {
                throw new ArgumentException("Error: used id cannot be null.");
            }
            else if(HasLiked(user))
            {
                throw new InvalidOperationException($"Error: User {user.UserId} has already liked the post.");
            }
            else
            {
                Likes.Add(user.UserId);
            }
        }

        /**
         * Removes user from the list of likes.
         * <exception cref="ArgumentException">Thrown if hte user id is null or empty.</exception>
         * <exception cref="InvalidOperationException">Thrown if the user is not in the list of likes.</exception>
         *
        public void Dislike(User user)
        {
            if (user.UserId == null)
            {
                throw new ArgumentException("Error: used id cannot be null.");
            }
            else if( ! HasLiked(user) )
            {
                throw new InvalidOperationException($"Error: User {user.UserId} has not liked the post.");
            }
        }

        public bool HasLiked(User user)
        {
            return Likes.Contains(user.UserId);
        }*/
    }
}
