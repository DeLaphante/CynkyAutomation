using CynkyAutomation.Models.UI;
using CynkyAutomation.PageObjects.CommonPages;
using CynkyWrapper;
using OpenQA.Selenium;

namespace CynkyAutomation.PageObjects.OrangeCRM
{
    public class CRMLoginPage : Navigation
    {

        public CRMLoginPage(IWebDriver driver) : base(driver)
        {
            _Driver = driver;
        }

        #region Locators

        PageElement Username_textbox => new PageElement(_Driver, By.XPath("//input[@name='username']"));
        PageElement Password_textbox => new PageElement(_Driver, By.XPath("//input[@name='password']"));

        #endregion

        #region Actions

        public void Login(CrmUser crmUser)
        {
            Username_textbox.SendKeys(crmUser.Username);
            Password_textbox.SendKeys(crmUser.Password);
            ClickButton("Login");
        }

        #endregion
    }
}