using Flow.Core.DomainModels;
using System.Threading.Tasks;

namespace FlowUI.Pages
{
    public partial class NewPostForm
    {
        public Post Post { get; set; } = new Post();

        public bool ShowDialog { get; set; } = false;

        protected override void OnInitialized()
        {
            ShowDialog = false;
        }

        public void HandleValidSubmit()
        {
            CloseDialog();
        }

        public void HandleInvalidSubmit()
        {

        }

        public void OpenDialog()
        {
            if(!ShowDialog)
            {
                ResetDialog();
                ShowDialog = true;
                StateHasChanged();
            }
        }

        public void CloseDialog()
        {
            if(ShowDialog)
            {
                ResetDialog();
                ShowDialog = false;
                StateHasChanged();
            }
        }

        private void ResetDialog()
        {
            Post = new Post();
        }
    }
}
