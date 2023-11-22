using InstagramAutomatization.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramAutomatization.Services.Abstractions
{
    internal interface IInstagram
    {
        public bool GetStatusLogin();
        public IInstagramUser Login(string login, string password);
        public UserInfo GetUserInfo(string username);
        public List<UserInfo> GetUsersInfo(List<string> usernames);
    }
}
