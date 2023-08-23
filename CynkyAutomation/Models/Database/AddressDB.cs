namespace CynkyAutomation.Models.Database
{
    public class AddressDB
    {
        public string id { get; set; }
        public int Version { get; set; }
        public string AddressSingleLine { get; set; }
        public string BuildingNumber { get; set; }
        public string BuildingName { get; set; }
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string Locality { get; set; }
        public string SubLocality { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string CountryCodeISO2 { get; set; }
        public string BusinessName { get; set; }
        public string ApartmentName { get; set; }
        public string BlockedDateTimeUTC { get; set; }
    }
}
