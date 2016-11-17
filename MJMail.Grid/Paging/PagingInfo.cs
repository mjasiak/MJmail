namespace MJMail.Grid.Paging
{
    public class PagingInfo
    {
        public int pageNumber { get; set; }
        public int pageTotal { get; set; }
        public int pageSize { get; set; }
        public string action { get; set; }
        public string controller { get; set; }
        public string searchString { get; set; }

        public void getTotal(int dataSize)
        {
            if (dataSize % pageSize == 0) pageTotal = dataSize / pageSize;
            else pageTotal = dataSize / pageSize + 1;
        }       
        public PagingInfo SetPagingInfo(int? currentPage, string schString, int itemOnPage, int dataSize, string actionName, string controllerName)
        {
            pageNumber = currentPage ?? 1;
            pageSize = itemOnPage;
            getTotal(dataSize);
            action = actionName;
            controller = controllerName;
            searchString = schString;
            return this;
        }
        public PagingInfo SetPagingInfo(int? currentPage, string schString, int itemOnPage, int dataSize)
        {
            pageNumber = currentPage ?? 1;
            pageSize = itemOnPage;
            getTotal(dataSize);
            searchString = schString;
            return this;
        }
        public PagingInfo SetPagingInfo(int? currentPage, int itemOnPage, int dataSize, string actionName, string controllerName)
        {
            pageNumber = currentPage ?? 1;
            pageSize = itemOnPage;
            getTotal(dataSize);
            action = actionName;
            controller = controllerName;
            return this;
        }
    }
}
