using CynkyUtilities;

namespace CynkyAutomation.Models.UI
{
    public class EmployeeProfile
    {
        public string Firstname { get; set; } = $"{StringGenerator.GetRandomString()}";
        public string Lastname { get; set; } = $"{StringGenerator.GetRandomString()}";
    }
}
