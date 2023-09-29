using CsvHelper.Configuration.Attributes;

namespace CynkyAutomation.Models.UI
{
    public class Postcodes
    {
        [Name("postcode")]
        public string Postcode { get; set; }
    }
}
