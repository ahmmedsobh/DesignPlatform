namespace DesignPlatform.ViewModels.PaginationViewModels
{
    public class PaginationParamsViewModel
    {
        public PaginationParamsViewModel()
        {
            MaxRows = 10;
            PageNumber = 1;
        }
        public int MaxRows { get; set; }
        public int PageNumber { get; set; }
        public string SearchString { get; set; }
        public string Mails { get; set; }
    }
}
