using CynkyAutomation.PageObjects.CommonPages;
using OpenQA.Selenium;

namespace CynkyAutomation.PageObjects.OrangeHRM
{
    public class SideNavBar : Navigation
    {
        public SideNavBar(IWebDriver driver) : base(driver) { }

        #region Actions

        public void ClickOnOption(string option)
        {
            ClickLink(option);
        }

        public bool IsOptionDisplayed(string text)
        {
            return Button(text).IsDisplayed();
        }

        #endregion
    }
}