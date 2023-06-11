namespace CynkyAutomation.Models.API.Response
{
    public class CreateBookingResponse
    {
        public int bookingid { get; set; }
        public Booking booking { get; set; }
    }

    public class Booking
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int totalprice { get; set; }
        public bool depositpaid { get; set; }
        public Bookingdates bookingdates { get; set; }
        public string additionalneeds { get; set; }
    }
}