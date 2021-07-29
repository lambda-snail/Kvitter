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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }

        /** The date and time when the user signed up. */
        public DateTimeOffset SignUpTime { get; set; }

        public byte[] ProfilePicture { get; set; }

        public List<FriendModel> Friends { get; set; }

        //public ICollection<User> BlockedUsers { get; set; }
    }
}
