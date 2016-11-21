using MJmail.Data;
using MJmail.Models;
using MJMail.Data.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJMail.Methods.Messages
{
    public interface IAdvancedSearch
    {
        List<Message> FindMessages(AdvancedSearchQuery query, MaildbContext context);
    }
}
