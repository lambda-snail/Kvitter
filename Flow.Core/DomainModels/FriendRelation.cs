using System;
using System.Collections.Generic;

namespace Flow.Core.DomainModels
{
    /**
     * A FriendRelation is a pair of User objects, A and B, that abstracts the bidirectional relation that
     * A "is friends with" B.
     */
    public class FriendRelation
    {
        public object Id { get; init; }
        public List<Guid> Users { get; set; }
        public DateTimeOffset ConnectedDate { get; set; }
    }
}
