using CsvHelper;

namespace CynkyAutomation.Models.UI
{
    [CsvRecord]
    public partial class Postcodes
    {
        [CsvColumn("postcode")]
        public string Postcode { get; set; }
    }
}
