using CynkyAutomation.PageObjects.CommonPages;
using CynkyDriver;
using OpenQA.Selenium;

namespace CynkyAutomation.PageObjects.OrangeHRM
{
    public class TopNavBar : Navigation
    {
        public TopNavBar(IWebDriver driver) : base(driver) { }

        #region Locators

        PageElement Menu_dropdown => new PageElement(_Driver, By.XPath($"//li//p"));
        PageElement Header_label => new PageElement(_Driver, By.XPath($"//h6"));

        #endregion

        #region Actions

        public void ClickOnMenuOption(string option)
        {
            Menu_dropdown.Click();
            ClickButton(option);
        }

        public string GetHeader()
        {
            return Header_label.GetText();
        }

        #endregion
    }
}