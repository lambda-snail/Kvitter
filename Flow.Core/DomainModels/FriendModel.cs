using System;

namespace Flow.Core.DomainModels
{
    /**
     * A subset of the User class intended to be used in a list of friends.
     */
    public class FriendModel
    {
        public Guid UserId { get; set; }
        public Object FriendRelationId { get; set; }

        public string FirstName { get; set; }
        public string ShortDescription { get; set; }
        public DateTimeOffset WhenConnected { get; set; }
    }
}
