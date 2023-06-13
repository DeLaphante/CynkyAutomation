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

        [Given(@"user is on the Orange CRM homepage")]
        [When(@"user is on the Orange CRM homepage")]
        [Then(@"user is on the Orange CRM homepage")]
        public void GivenINavigateToOrangeCRMTheHomePage()
        {
            _Navigation.NavigateToOrangeCRMLandingPage();
        }
    }
}
