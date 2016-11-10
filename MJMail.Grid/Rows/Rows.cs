using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJMail.Grid.Rows
{
    public class Rows
    {
        static List<Row> rows = new List<Row>();

        public static void AddRow(Row row)
        {
            rows.Add(row);
        }
    }
}
