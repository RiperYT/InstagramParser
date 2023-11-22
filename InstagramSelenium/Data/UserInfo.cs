using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramAutomatization.Data
{
    internal class UserInfo
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public UserInfo(string username)
        {
            UserName = username;
            Name = "";
            Description = "";
            Link = "";
        }

        public UserInfo(string username, string name, string description, string link)
        {
            UserName = username;
            Name = name;
            Description = description;
            Link = link;
        }
    }
}
