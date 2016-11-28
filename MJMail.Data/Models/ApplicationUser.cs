using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MJMail.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [JsonIgnore]
        public virtual ICollection<ChatFriend> ChatFriends { get; set; }
    }
}
