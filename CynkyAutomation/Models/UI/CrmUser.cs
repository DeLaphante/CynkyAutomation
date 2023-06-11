using CynkyUtilities;

namespace CynkyAutomation.Models.UI
{
    public class CrmUser
    {
        public string Username { get; set; } = StringGenerator.GetRandomString();
        public string Password { get; set; } = StringGenerator.GetRandomString();
    }
}
