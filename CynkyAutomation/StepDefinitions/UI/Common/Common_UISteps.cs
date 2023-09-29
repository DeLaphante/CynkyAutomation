using CynkyAutomation.PageObjects.CommonPages;
using TechTalk.SpecFlow;

namespace CynkyAutomation.StepDefinitions.UI.Common
{
    [Binding]
    public class Common_UISteps
    {
        Navigation _Navigation;

        public Common_UISteps(ScenarioContext scenarioContext)
        {
            _Navigation = scenarioContext.ScenarioContainer.Resolve<Navigation>();
        }

        [StepDefinition(@"user is on the Orange HRM homepage")]
        public void GivenINavigateToOrangeHRMTheHomePage()
        {
            _Navigation.NavigateToOrangeHRMLandingPage();
        }
    }
}
