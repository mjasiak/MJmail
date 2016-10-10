using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MJmail.Models
{
    public class Message
    {
        public int ID { get; set; }

        public string MailTitle { get; set; }

        public string MailContent { get; set; }

        public string MailFrom { get; set; }

        public string MailTo { get; set; }

        public string MailDate { get; set; }
    }
}