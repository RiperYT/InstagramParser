using InstagramAutomatization.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramAutomatization.Services.Abstractions
{
    internal interface IInstagramUser
    {
        public long GetFollowersCount();
        public List<string> GetFollowersUsernames();
    }
}
