using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJMail.Grid.Paging
{
    public interface IPagingInfo
    {
        PagingInfo SetPagingInfo(int? currentPage, string schString, int itemOnPage, int dataSize, string actionName, string controllerName);
        //int getPageSize();
        int pageSize { get; set; }
    }
}
