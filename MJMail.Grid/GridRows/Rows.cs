using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJMail.Grid.GridRows
{
    public class Rows
    {
        List<Row> rows = new List<Row>();
        public void AddRow(Row row)
        {
            rows.Add(row);
        }

        public List<Row> GetRows()
        {
            return rows;
        }
    }
}
