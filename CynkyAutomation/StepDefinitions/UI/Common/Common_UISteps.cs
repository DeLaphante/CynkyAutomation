using BoDi;
using CynkyAutomation.PageObjects.CommonPages;
using TechTalk.SpecFlow;

namespace CynkyAutomation.StepDefinitions.UI.Common
{
    [Binding]
    public sealed class Common_UISteps
    {
        Navigation _Navigation;

        public Common_UISteps(IObjectContainer objectContainer)
        {
            _Navigation = objectContainer.Resolve<Navigation>();
        }

        [Given(@"customer is on the Orange CRM homepage")]
        [When(@"customer is on the Orange CRM homepage")]
        [Then(@"customer is on the Orange CRM homepage")]
        public void GivenINavigateToOrangeCRMTheHomePage()
        {
            _Navigation.NavigateToOrangeCRMLandingPage();
        }
    }
}
