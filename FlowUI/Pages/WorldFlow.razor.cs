//using Microsoft.AspNetCore.Components.Web.Virtualization;

using Flow.Core.Contracts;
using Flow.Core.DomainModels;
using Flow.Core.Mediate.DatabaseSize;
using Flow.Core.Mediate.GetPosts;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.Threading.Tasks;

namespace FlowUI.Pages
{
    public partial class WorldFlow
    {
        public NewPostForm NewPostForm { get; set; }

        public Virtualize<Post> PostVirtualizer { get; set; }

        [Inject]
        protected IMediator _mediator { get; set; }

        [Inject]
        protected IPostRepository _repository { get; set; }

        /**
         * Things that might need to be changed if the application is large/has many users:
         * - Handling the total count of posts (minimize number of requests).
         * - Conversion from long to int might not always work.
         */
        public async ValueTask<ItemsProviderResult<Post>> LoadPostsPaged(ItemsProviderRequest request)
        {
            var posts = await _mediator.Send(new GettAllPostsPagedRequest { Skip=request.StartIndex, Take=request.Count });
            var totalPostCount = await _mediator.Send(new EstimatePostCountRequest());
            return new ItemsProviderResult<Post>(posts, (int)totalPostCount); // This might need tweaking if the application grows
        }

        /**
         * Callback for when a post is created. Refresh the component so that the new post is shown.
         */
        public async void OnPostCreatedAsync()
        {

            await PostVirtualizer.RefreshDataAsync();
            StateHasChanged();
        }

        public void OpenNewPostForm()
        {
            NewPostForm.OpenDialog();
        }
    }
}
