using DesignPlatform.ViewModels.PaginationViewModels;

namespace DesignPlatform.Helpers
{
    public class PaginationHelper
    {
        public static PaginationDataViewModel<T> GetPaginationData<T>(IQueryable<T> Data, int MaxRows, int RowsCount, int PageNumber)
        {

            var countRemaine = RowsCount / MaxRows;
            var PagesCount = RowsCount % MaxRows != 0 ? ++countRemaine : countRemaine;

            if (PagesCount == 0)
            {
                PagesCount = 1;
            }

            var NextPage = PagesCount == PageNumber ? PageNumber : PageNumber + 1;
            var PreviousPage = PageNumber == 1 ? PageNumber : PageNumber - 1;

            Data = Data.Skip((PageNumber - 1) * MaxRows).Take(MaxRows);


            var data = new PaginationDataViewModel<T>()
            {
                PageNumber = PageNumber,
                PagesCount = PagesCount,
                NextPage = NextPage,
                PreviousPage = PreviousPage,
                Data = Data,
            };

            return data;

        }
    }
}
