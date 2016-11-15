using MJMail.Grid.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJMail.Grid.GridRows
{
    public class Row
    {
        string ID { get; set; }
        public string _prefix { get; set; }
        public string _postfix { get; set; }
        List<Cell> cells = new List<Cell>();

        public Row(string prefix, string postfix)
        {
            _prefix = prefix;
            _postfix = postfix;
        }

        public void AddCell(Cell cell)
        {
            cells.Add(cell);
        }

        public List<Cell> GetCells()
        {
            return cells;
        }
    }
}
