using CsvHelper;

namespace CynkyAutomation.Models.UI
{
    public class Postcodes
    {
        [CsvColumn("postcode")]
        public string Postcode { get; set; }
    }
}
