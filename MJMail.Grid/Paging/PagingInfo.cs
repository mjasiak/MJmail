using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJMail.Grid.Paging
{
    public class PagingInfo
    {
        public int pageNumber { get; set; }
        int pageTotal { get; set; }
        public int pageSize { get; set; }

        public void getTotal(int dataSize)
        {
            if (dataSize % pageSize == 0) pageTotal = dataSize / pageSize;
            else pageTotal = dataSize / pageSize + 1;
        }
    }
}
