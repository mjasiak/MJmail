using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJMail.Data.TDO
{
    public class AdvancedSearchQuery
    {
        public string MailFrom { get; set; }
        public string MailTo { get; set; }
        public string MailTitle { get; set; }
        public string MailHasWords { get; set; }
        public string MailDoesntHave { get; set; }
    }
}
