namespace CynkyAutomation.EndpointBuilder
{
    public class BookingEndpointBuilder
    {
        public static string GetAuthEndPoint()
        {
            return $"auth";
        }

        public static string GetBookingEndPoint()
        {
            return $"booking";
        }

        public static string GetBookingEndPoint(string id)
        {
            return $"booking/{id}";
        }

        public static string GetPingEndPoint()
        {
            return $"ping";
        }
    }
}
