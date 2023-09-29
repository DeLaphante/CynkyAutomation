using CynkyAutomation.PageObjects.CommonPages;
using CynkyWrapper;
using OpenQA.Selenium;

namespace CynkyAutomation.PageObjects.OrangeHRM
{
    public class SideNavBar : Navigation
    {
        public SideNavBar(IWebDriver driver) : base(driver)
        {
            _Driver = driver;
        }

        #region Locators

        PageElement Option_link(string text) => new PageElement(_Driver, By.XPath($"//a[contains(.,\"{text}\")]"));

        #endregion

        #region Actions

        public void ClickOnOption(string option)
        {
            ClickLink(option);
        }

        public bool IsOptionDisplayed(string text)
        {
            return Option_link(text).IsDisplayed();
        }

        #endregion
    }
}