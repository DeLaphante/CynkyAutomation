using CynkyWrapper;
using OpenQA.Selenium;

namespace CynkyAutomation.PageObjects.CommonPages
{
    public class MailinatorPage
    {
        IWebDriver _Driver;

        public MailinatorPage(IWebDriver driver)
        {
            _Driver = driver;
        }

        #region Locators

        PageElement Inbox_link(string text) => new PageElement(_Driver, By.XPath($"(//tr[contains(.,'a min ago') or contains(.,'just now')]//" +
            $"a[contains(.,'{text}')])[1]"));
        PageElement Iframe => new PageElement(_Driver, By.Id("msg_body"));

        #endregion

        #region Actions

        public void ClickLink(string text)
        {
            Inbox_link(text).Click();
        }

        #endregion
    }
}