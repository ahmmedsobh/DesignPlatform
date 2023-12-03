namespace DesignPlatform.ViewModels.MessagesViewModels
{
    public class MessagesListViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public bool IsMyMessage { get; set; }

    }
}
