using CynkyWrapper;
using CynkyWrapper.Configuration;
using OpenQA.Selenium;
using System.Threading;

namespace CynkyAutomation.PageObjects.CommonPages
{
    public class Navigation
    {
        protected IWebDriver _Driver;
        dynamic _windows;

        public Navigation(IWebDriver driver)
        {
            _Driver = driver;
        }

        #region Locators

        PageElement AcceptCookie_button => new PageElement(_Driver, By.Id("consent_prompt_submit"));
        PageElement NavigateTo(string page) => new PageElement(_Driver, By.PartialLinkText($"{page}"));
        PageElement Button(string text, int index) => new PageElement(_Driver, By.XPath($"(//button[translate(normalize-space(.), " +
            $"'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')=\"{text.ToLower()}\"])[{index}]"));
        PageElement Link(string text) => new PageElement(_Driver, By.PartialLinkText($"{text}"));
        PageElement PageHeader1_label(string text) => new PageElement(_Driver, By.XPath($"//h1[text()=\"{text}\"]"));
        PageElement PageHeader2_label(string text) => new PageElement(_Driver, By.XPath($"//h2[translate(text(), " +
            $"'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')=\"{text.ToLower()}\"]"));
        PageElement PageParagraph_label(string text) => new PageElement(_Driver, By.XPath($"//p[translate(text(), " +
                    $"'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')=\"{text.ToLower()}\"]"));
        PageElement PageTag_label(string text) => new PageElement(_Driver, By.TagName($"{text}"));
        PageElement Text_label(string text) => new PageElement(_Driver, By.XPath($"//*[contains(.,\"{text}\")]"));
        PageElement DesktopAccountIcon_button => new PageElement(_Driver, By.XPath($"(//div[contains(@class,'desktop-menu')]//span)[1]"));
        PageElement MobileMenuIcon_button => new PageElement(_Driver, By.XPath($"//button[@data-testid='modal-open-button']"));

        #endregion

        #region Actions

        void AcceptCookie()
        {
            if (AcceptCookie_button.ElementExists())
                AcceptCookie_button.Click();
        }

        public void NavigateToOrangeCRMLandingPage()
        {
            _Driver.Navigate().GoToUrl(CynkyConfigManager.SiteUrl);
        }

        void NavigateToMailinatorLandingPage()
        {
            _Driver.Navigate().GoToUrl("https://www.mailinator.com");
        }

        void NavigateToUrl(string url)
        {
            _Driver.Navigate().GoToUrl(url);
        }

        void HandleMobileView()
        {
            if (CynkyConfigManager.Browser.ToLower() == "mobile"
               || CynkyConfigManager.ExecutionPlatform.ToLower() == "mobile")
            {
                MobileMenuIcon_button.Click();
            }
        }

        void NavigateToPage(string page)
        {
            HandleMobileView();
            NavigateTo(page).Click();
        }

        protected void ClickButton(string text, int index = 1)
        {
            Button(text, index).Click();
        }

        protected void ClickLink(string text)
        {
            Link(text).Click();
        }

        protected void OpenNewTabWithUrl(string url, int windowsindex)
        {
            IJavaScriptExecutor _js = (IJavaScriptExecutor)_Driver;
            _js.ExecuteScript($@"window.open('{url}', '_blank');");
            _windows = _Driver.WindowHandles;
            _Driver.SwitchTo().Window(_windows[windowsindex]);
        }

        protected void CloseActiveTab(int index)
        {
            _Driver.Close();
            _Driver.SwitchTo().Window(_windows[index]);
        }

        bool IsPageParaGraphDisplayed(string text)
        {
            return PageParagraph_label(text).IsDisplayed();
        }
        bool IsPageHeaderOneDisplayed(string header)
        {
            return PageHeader1_label(header).IsDisplayed();
        }

        bool IsPageHeaderTwoDisplayed(string header)
        {
            return PageHeader2_label(header).IsDisplayed();
        }

        bool IsButtonDisplayed(string text, int index = 1)
        {
            return Button(text, index).IsDisplayed();
        }

        bool ButtonExists(string text, int index = 1)
        {
            return Button(text, index).ElementExists();
        }

        bool IsLinkEnabled(string text)
        {
            return Link(text).IsEnabled();
        }

        bool IsButtonEnabled(string text, int index = 1)
        {
            return Button(text, index).IsEnabled();
        }

        void RefreshPage()
        {
            _Driver.Navigate().Refresh();
        }

        string GetCurrentURL()
        {
            return _Driver.Url;
        }

        void BrowserGoBack()
        {
            _Driver.Navigate().Back();
        }

        void ClickAlert()
        {
            _Driver.SwitchTo().Alert().Accept();
        }

        void SwitchToTab(int tabIndex)
        {
            _Driver.SwitchTo().Window(_Driver.WindowHandles[tabIndex]);
        }

        void WaitFor(int seconds)
        {
            Thread.Sleep(1000 * seconds);
        }

        #endregion
    }
}