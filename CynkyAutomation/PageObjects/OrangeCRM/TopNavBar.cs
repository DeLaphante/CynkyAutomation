using CynkyAutomation.PageObjects.CommonPages;
using CynkyWrapper;
using OpenQA.Selenium;

namespace CynkyAutomation.PageObjects.OrangeCRM
{
    public class TopNavBar : Navigation
    {
        public TopNavBar(IWebDriver driver) : base(driver)
        {
            _Driver = driver;
        }

        #region Locators

        PageElement Menu_dropdown => new PageElement(_Driver, By.XPath($"//li//p"));
        PageElement Header_label => new PageElement(_Driver, By.XPath($"//h6"));
        PageElement Option_link(string text) => new PageElement(_Driver, By.XPath($"//a[text()=\"{text}\"]"));

        #endregion

        #region Actions

        public void ClickOnMenuOption(string option)
        {
            Menu_dropdown.Click();
            Option_link(option).Click();
        }

        public string GetHeader()
        {
            return Header_label.GetText();
        }

        #endregion
    }
}