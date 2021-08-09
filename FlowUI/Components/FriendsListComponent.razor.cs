using Flow.Core.DomainModels;
using Microsoft.AspNetCore.Components;

namespace FlowUI.Components
{
    /**
     * Displays a list of the friends of a given user.
     */
    public partial class FriendsListComponent
    {
        [Parameter]
        public User User { get; set; }
    }
}
