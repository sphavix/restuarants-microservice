namespace Restuarants.Application.Common
{
    public class PagedResult<T>
    {
        public PagedResult(IEnumerable<T> items, int totalItemsCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItemsCount = totalItemsCount;
            TotalPages = (int)Math.Ceiling(totalItemsCount / (double)pageSize);
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemsFrom + pageSize - 1;

            // page size = 5, page number = 2
            // skip: pageSize * (pageNumber -1) == 5
            // itemsFrom: 5 + 1 = 6
            // itemsTo: 6 + 5 - 1 = 10
        }

        public IEnumerable<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int TotalItemsCount { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
    }
}
