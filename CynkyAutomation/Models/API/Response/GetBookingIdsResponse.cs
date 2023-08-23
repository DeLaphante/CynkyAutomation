namespace CynkyAutomation.Models.API.Response
{
    public class GetBookingIdsResponse
    {
        public int? bookingid { get; set; }
    }

    public class Bookingdates
    {
        public string checkin { get; set; }
        public string checkout { get; set; }
    }
}