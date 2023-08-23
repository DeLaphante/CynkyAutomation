using CynkyUtilities;

namespace CynkyAutomation.Models.UI
{
    public class PersonalDetails
    {
        public string Firstname { get; set; } = $"{StringGenerator.GetRandomString()}";
        public string MiddleName { get; set; } = $"{StringGenerator.GetRandomString()}";
        public string LastName { get; set; } = $"{StringGenerator.GetRandomString()}";
        public string NickName { get; set; } = $"{StringGenerator.GetRandomString()}";
        public int EmployeeId { get; set; }
        public int OtherId { get; set; }
        public int DriverLicenseNumber { get; set; }

    }
}
