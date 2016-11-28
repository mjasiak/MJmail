using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJMail.Data.Models
{
    public class ChatFriend
    {
        public int ID { get; set; }
        public string Friend { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
