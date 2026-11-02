using CsvHelper;
using CynkyAutomation.Models.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CynkyAutomation.Utilities
{
    public class PostCodeClient
    {
        static string _projectPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            AppDomain.CurrentDomain.RelativeSearchPath ?? string.Empty);

        static string _testDatafolder = _projectPath + "Data/Files/Postcodes/";

        public static List<Postcodes> GetUkPostCodes()
        {
            using (var csv = CsvReader.FromFile($"{_testDatafolder}/ukpostcodes.csv"))
            {
                return csv.GetRecords<Postcodes>().ToList();
            }
        }
    }
}
