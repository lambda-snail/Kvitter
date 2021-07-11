using System;

namespace Flow.Core.DomainModels
{
    /**
     * A class representing a comment made by a user. It is intended to be embedded in other objects that
     * support commenting, so it has no id of its own. 
     */
    public class Comment
    {
        public Guid CommentOwnerId { get; set; }
        public string Content { get; set; }
    }
}
