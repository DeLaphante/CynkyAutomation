using CsvHelper;
using CynkyAutomation.Models.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CynkyAutomation.Utilities
{
    public class PostCodeClient
    {
        static string _projectPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            AppDomain.CurrentDomain.RelativeSearchPath ?? string.Empty);

        static string _testDatafolder = _projectPath + "/TestData/UI/Postcodes/";

        public static List<Postcodes> GetUkPostCodes()
        {
            using (var reader = new StreamReader($"{_testDatafolder}/ukpostcodes.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
            {
                return csv.GetRecords<Postcodes>().ToList();
            }
        }
    }
}
