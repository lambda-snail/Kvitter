using System;
using System.Collections.Generic;
using System.Text;

namespace Flow.Core.DomainModels
{
    /**
     * The class that represents a user in the system.
     */
    public class User
    {
        /** This user is linked to an IdentituUser via the user id, which is the same in both databases. */
        public Guid UserId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int Age { get; set; }
        public String Description { get; set; }

        //public ICollection<Post> Posts { get; set; }
        //public ICollection<User> Friends { get; set; }
        //public ICollection<User> BlockedUsers { get; set; }

    }
}
