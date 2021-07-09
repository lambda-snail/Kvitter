using System;

namespace FlowUI.ViewModels
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; } // Form cannot map to int
        public string Description { get; set; }
        public DateTimeOffset SignUpTime { get; set; }
    }
}
