using HonestFootball.Models;
using System.Threading.Tasks;

namespace HonestFootball.ViewModels
{
    public class CommentsViewModel : BaseViewModel
    {
        public Comment[] Comments { get; set; }

        public Comment Comment { get; set; }

        public async Task GetComments()
        {
            //if (Conversation == null)
            //    throw new Exception("No conversation.");

            IsBusy = true;

            try
            {
                Comments = await service.GetComments("TeamId");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
