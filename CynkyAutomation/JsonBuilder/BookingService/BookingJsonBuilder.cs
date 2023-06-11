using CynkyAutomation.Models.API.Request;
using CynkyAutomation.Models.API.Response;
using CynkyUtilities;

namespace CynkyAutomation.JsonBuilder.BookingService
{
    internal class BookingJsonBuilder
    {
        public static CreateBookingRequest GetCreateBookingData()
        {
            return new CreateBookingRequest
            {
                firstname = StringGenerator.GetRandomString(),
                lastname = StringGenerator.GetRandomString(),
                totalprice = StringGenerator.GetRandomNumberBetween(1, 1000),
                depositpaid = true,
                bookingdates = new Bookingdates
                {
                    checkin = StringGenerator.GetRandomDate(2011, 2015),
                    checkout = StringGenerator.GetRandomDate(2016, 2020),
                },
                additionalneeds = StringGenerator.GetRandomString()
            };
        }
    }
}
