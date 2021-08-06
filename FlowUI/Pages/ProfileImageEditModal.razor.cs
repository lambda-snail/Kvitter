using Flow.Core.Contracts;
using Flow.Core.DomainModels;
using Flow.Core.Mediate.UpsertUser;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace FlowUI.Pages
{
    public partial class ProfileImageEditModal
    {
        public bool ShowDialog { get; set; } = false;

        [Inject]
        private IMediator _mediator { get; set; }

        [Inject]
        private ILoggedInUserService _loggedInUserService { get; set; }

        public User LoggedInUser { get; set; }

        [Parameter]
        public EventCallback StateChangedNotifyParentComponent { get; set; }

        protected override void OnInitialized()
        {
            ShowDialog = false;
        }

        public async Task HandleValidSubmit()
        {
            await _mediator.Send(new UpsertUserRequest { User = LoggedInUser });
            await StateChangedNotifyParentComponent.InvokeAsync();
            CloseDialog();
        }

        public async Task HandleInvalidSubmit()
        {
            
        }

        public async Task OpenDialog()
        {
            if(LoggedInUser == null)
            {
                LoggedInUser = await _loggedInUserService.GetLoggedInUser();
            }

            if (LoggedInUser != null)
            {
                if (!ShowDialog)
                {
                    ResetDialog();
                    ShowDialog = true;
                    StateHasChanged();
                }
            }
        }

        public void CloseDialog()
        {
            if (ShowDialog)
            {
                ResetDialog();
                ShowDialog = false;
                StateHasChanged();
            }
        }

        private void ResetDialog()
        {
        }

        private async Task ProfilePictureSelected(InputFileChangeEventArgs evt)
        {
            var i = await evt.File.RequestImageFileAsync("jpg", 250, 250);

            var x = i.OpenReadStream();
            byte[] img = new byte[x.Length];
            await x.ReadAsync(img, 0, img.Length);

            LoggedInUser.ProfilePicture = img;

            //Image = "data:image/png;base64," + Convert.ToBase64String(img, 0, img.Length);
        }
    }
}
