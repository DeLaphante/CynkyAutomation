using BoDi;
using CynkyAutomation.Models.UI;
using CynkyAutomation.PageObjects.OrangeCRM;
using FluentAssertions;
using System.Diagnostics;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CynkyAutomation.StepDefinitions.UI
{
    [Binding]
    public class OrangeCRM_UISteps
    {

        MyInfoPage _MyInfoPage;
        CRMLoginPage _CRMLoginPage;
        PIMPage _PIMPage;
        TopNavBar _TopNavBar;
        SideNavBar _SideNavBar;
        ScenarioContext _ScenarioContext;

        public OrangeCRM_UISteps(IObjectContainer objectContainer)
        {
            _MyInfoPage = objectContainer.Resolve<MyInfoPage>();
            _CRMLoginPage = objectContainer.Resolve<CRMLoginPage>();
            _PIMPage = objectContainer.Resolve<PIMPage>();
            _TopNavBar = objectContainer.Resolve<TopNavBar>();
            _SideNavBar = objectContainer.Resolve<SideNavBar>();
            _ScenarioContext = objectContainer.Resolve<ScenarioContext>();
        }

        [Given(@"user with the following details logs in:")]
        [When(@"user with the following details logs in:")]
        [Then(@"user with the following details logs in:")]
        public void WhenUserWithTheFollowingDetailsLogsIn(Table table)
        {
            var crmUser = table.CreateInstance<CrmUser>();
            _ScenarioContext.Set<CrmUser>(crmUser, "crmUser");
            _CRMLoginPage.Login(crmUser);
        }

        [Given(@"user navigates to '([^']*)' Page")]
        [When(@"user navigates to '([^']*)' Page")]
        [Then(@"user navigates to '([^']*)' Page")]
        public void WhenNaviagtesToPage(string page)
        {
            if (!_TopNavBar.GetHeader().Equals(page))
                _SideNavBar.ClickOnOption(page);
        }


        [Given(@"user deletes employee in row without employment status '([^']*)'")]
        [When(@"user deletes employee in row without employment status '([^']*)'")]
        [Then(@"user deletes employee in row without employment status '([^']*)'")]
        public void WhenUserDeletesEmployeeInRowNumber(string status)
        {
            _ScenarioContext.Set<string>(_PIMPage.GetFirstNameOfEmployeeWithoutEmploymentStatus(status), "employee");
            var stopwatch = Stopwatch.StartNew();
            do
            {
                _PIMPage.DeleteUser(_ScenarioContext.Get<string>("employee"));
            } while (_PIMPage.IsUserDisplayed(_ScenarioContext.Get<string>("employee")) && stopwatch.ElapsedMilliseconds < 300000);
        }

        [Given(@"the employee should not be displayed")]
        [When(@"the employee should not be displayed")]
        [Then(@"the employee should not be displayed")]
        public void ThenTheEmployeeShouldNotBeDisplayed()
        {
            _PIMPage.IsUserDisplayed(_ScenarioContext.Get<string>("employee")).Should().BeFalse();
            _TopNavBar.ClickOnMenuOption("Logout");
            _CRMLoginPage.Login(_ScenarioContext.Get<CrmUser>("crmUser"));
            _SideNavBar.IsOptionDisplayed("PIM").Should().BeTrue();
        }

        [Given(@"user adds a new employee")]
        [When(@"user adds a new employee")]
        [Then(@"user adds a new employee")]
        public void WhenUserAddsANewEmployee()
        {
            var employee = new { EmployeeProfile.Firstname, EmployeeProfile.Lastname };
            _ScenarioContext.Set<string>(employee.Firstname, "firstname");
            _ScenarioContext.Set<string>(employee.Lastname, "lastname");
            _PIMPage.AddEmployee(employee.Firstname, employee.Lastname);
            _SideNavBar.ClickOnOption("PIM");
        }

        [Given(@"the employee can be seen on the list")]
        [When(@"the employee can be seen on the list")]
        [Then(@"the employee can be seen on the list")]
        public void ThenTheEmployeeCanBeSeenOnTheList()
        {
            var listOfEmployees = _PIMPage.GetAllEmployees();
            listOfEmployees.Contains(_ScenarioContext.Get<string>("firstname"));
            listOfEmployees.Contains(_ScenarioContext.Get<string>("lastname"));
        }

        [When(@"user updates info")]
        public void WhenUserUpdatesInfo()
        {
            var details = new PersonalDetails();
            _ScenarioContext.Set(details);
            _MyInfoPage.UpdateInfo(details);
        }

        [Then(@"the info is updated successfully")]
        public void ThenTheInfoIsUpdatedSuccessfully()
        {
            _MyInfoPage.GetTextFromInputField("firstName").Should().Be(_ScenarioContext.Get<PersonalDetails>().Firstname);
        }

    }
}
