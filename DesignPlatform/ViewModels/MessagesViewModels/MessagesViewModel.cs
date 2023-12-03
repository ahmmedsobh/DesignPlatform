namespace DesignPlatform.ViewModels.MessagesViewModels
{
    public class MessagesViewModel
    {
        public List<MessageUsersListViewModel> Users { get; set; }
        public List<MessagesListViewModel> Messages { get; set; }
        public string Name { get; set; }
    }
}
