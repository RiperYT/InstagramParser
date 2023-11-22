using CsvHelper;
using InstagramAutomatization.Data;
using InstagramAutomatization.Services.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramAutomatization.Services
{
    internal class SaveJSON : ISave
    {
        //Return path to file
        public string SaveUsersInfo(List<UserInfo> info)
        {

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            DirectoryInfo d = new DirectoryInfo(path);

            var Files = Directory.GetFiles(path);

            var p = false;
            var i = 0;

            while (!p)
            {
                i++;
                p = true;
                foreach (var file in Files)
                    if (file.Equals(path + "\\filePersons" + i + ".json"))
                    {
                        p = false;
                        break;
                    }
            }

            using (StreamWriter file = File.CreateText(path + "\\filePersons" + i + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, info);
            }

            return path + "\\filePersons" + i + ".json";
        }
    }
}
