using CynkyDriver;
using CynkyDriver.Configuration;
using OpenQA.Selenium;

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
        protected PageElement Button(string text, int index = 1) => new PageElement(_Driver, By.XPath($"(//*[(self::button or self::a or self::input or @onclick or @role='button')  and (contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),\"{text.ToLower()}\") or contains(@class,\"{text}\") or contains(@title,\"{text}\") or contains(@value,\"{text}\")) and not(contains(@class,'disable') or @disabled)])[{index}]"));
        PageElement Link(string text) => new PageElement(_Driver, By.PartialLinkText($"{text}"));
        PageElement PageHeader1_label(string text) => new PageElement(_Driver, By.XPath($"//h1[text()=\"{text}\"]"));
        PageElement PageHeader2_label(string text) => new PageElement(_Driver, By.XPath($"//h2[translate(text(), " +
            $"'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')=\"{text.ToLower()}\"]"));
        PageElement PageParagraph_label(string text) => new PageElement(_Driver, By.XPath($"//p[translate(text(), " +
                    $"'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')=\"{text.ToLower()}\"]"));
        PageElement MobileMenuIcon_button => new PageElement(_Driver, By.XPath($"//button[@data-testid='modal-open-button']"));

        #endregion

        #region Actions

        public void NavigateToOrangeHRMLandingPage()
        {
            _Driver.Navigate().GoToUrl(CynkyConfigManager.BaseSiteUrl);
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

        #endregion
    }
}