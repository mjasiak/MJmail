using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJMail.Grid.Cells
{
    public class Cell
    {
        string ID { get; set; }
        string _prefix { get; set; }
        string _postfix { get; set; }
        string content { get; set; }
        string _column { get; set; }
    }
}
