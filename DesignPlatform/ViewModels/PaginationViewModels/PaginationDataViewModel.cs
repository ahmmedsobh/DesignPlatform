namespace DesignPlatform.ViewModels.PaginationViewModels
{
    public class PaginationDataViewModel<T>
    {
        public int PagesCount { get; set; }
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }
        public int PageNumber { get; set; }
        public IQueryable<T> Data { get; set; }
    }
}
