using InstagramAutomatization.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramAutomatization.Services.Abstractions
{
    internal interface ISave
    {
        //Return path to file
        public string SaveUsersInfo(List<UserInfo> info);
    }
}
