using CynkyAutomation.Models.UI;
using CynkyAutomation.PageObjects.CommonPages;
using CynkyDriver;
using OpenQA.Selenium;

namespace CynkyAutomation.PageObjects.OrangeHRM
{
    public class HRMLoginPage : Navigation
    {
        public HRMLoginPage(IWebDriver driver) : base(driver) { }

        #region Locators

        PageElement Username_textbox => new PageElement(_Driver, By.XPath("//input[@name='username']"));
        PageElement Password_textbox => new PageElement(_Driver, By.XPath("//input[@name='password']"));
        PageElement Login_button => new PageElement(_Driver, By.XPath("//*[@type='submit']"));

        #endregion

        #region Actions

        public void Login(CrmUser crmUser)
        {
            Username_textbox.SendKeys(crmUser.Username);
            Password_textbox.SendKeys(crmUser.Password);
            Login_button.Click();
        }

        #endregion
    }
}