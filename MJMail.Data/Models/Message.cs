﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MJmail.Models
{
    public class Message
    {
        [Key]
        public int ID { get; set; }

        public string MailTitle { get; set; }

        [AllowHtml]
        public string MailContent { get; set; }

        public string MailFrom { get; set; }
        
        public string MailFromName { get; set; }

        public string MailTo { get; set; }

        public string MailToName { get; set; }

        public DateTime MailDate { get; set; }

        [NotMapped]
        public string EncodedID { get; set; }
    }
}