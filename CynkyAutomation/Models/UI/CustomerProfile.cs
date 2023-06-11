using CynkyUtilities;
using System.Collections.Generic;

namespace CynkyAutomation.Models.UI
{
    public class CustomerProfile
    {
        public static string Firstname { get; set; } = $"{StringGenerator.GetRandomString()}";
        public static string Lastname { get; set; } = $"{StringGenerator.GetRandomString()}";
        public string FullName { get; set; } = $"{Firstname} {Lastname}";
        public string Email { get; set; } = StringGenerator.GetRandomEmail("mailinator.com");
        public string MobilePhone { get; set; } = StringGenerator.GetRandomUkMobileNumber();
        public string Address1 { get; set; } = StringGenerator.GetRandomString();
        public string Postcode { get; set; } = "M9 6RU";
        public string CardType { get; set; } = "Visa";
        public string CardNumber { get; set; } = "4242424242424242";
        public string CardExpiryDate { get; set; } = $"01{DateTimeGenerator.GetTodaysDateTime().AddYears(1).ToString("yy")}";
        public string CardCvv { get; set; } = "123";
        public List<string> OrderIds { get; set; } = new List<string>() { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
    }
}
