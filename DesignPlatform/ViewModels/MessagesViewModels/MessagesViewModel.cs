namespace DesignPlatform.ViewModels.MessagesViewModels
{
    public class MessagesViewModel
    {
        public List<MessageUsersListViewModel> Users { get; set; }
        public List<MessagesListViewModel> Messages { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public bool NoSelectedUser { get; set; }
        public string SenderId { get; set; }
        public bool AllUsers { get; set; }
    }
}
