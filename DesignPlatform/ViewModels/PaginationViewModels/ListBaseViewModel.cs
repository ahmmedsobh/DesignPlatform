namespace DesignPlatform.ViewModels.PaginationViewModels
{
    public class ListBaseViewModel
    {
        public int PagesCount { get; set; }
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }
        public int PageNumber { get; set; }
        public bool EditModalShow { get; set; }
    }
}
