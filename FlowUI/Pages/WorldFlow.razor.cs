namespace FlowUI.Pages
{
    public partial class WorldFlow
    {
        public NewPostForm NewPostForm { get; set; }
        public void OpenNewPostForm()
        {
            NewPostForm.OpenDialog();
        }
    }
}
