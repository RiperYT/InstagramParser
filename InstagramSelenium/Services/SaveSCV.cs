using CsvHelper;
using InstagramAutomatization.Data;
using InstagramAutomatization.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramAutomatization.Services
{
    internal class SaveSCV : ISave
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
                    if (file.Equals(path + "\\filePersons" + i + ".csv"))
                    {
                        p = false;
                        break;
                    }
            }

            using (var writer = new StreamWriter(path + "\\filePersons" + i + ".csv", false, Encoding.UTF8))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(info);
            }
            return path + "\\filePersons" + i + ".csv";
        }
    }
}
