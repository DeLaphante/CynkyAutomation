using CynkyAutomation.Models.UI;
using CynkyAutomation.PageObjects.CommonPages;
using CynkyDriver;
using OpenQA.Selenium;

namespace CynkyAutomation.PageObjects.OrangeHRM
{
    public class MyInfoPage : Navigation
    {
        public MyInfoPage(IWebDriver driver) : base(driver) { }

        #region Locators

        PageElement InputField_textbox(string name) => new PageElement(_Driver, By.XPath($"//input[@name='{name}']"));

        #endregion

        #region Actions

        public void UpdateInfo(PersonalDetails personalDetails)
        {
            InputField_textbox("firstName").SendKeys(personalDetails.Firstname);
            ClickButton("Save");
        }

        public string GetTextFromInputField(string fieldName)
        {
            return InputField_textbox(fieldName).GetDomAttribute("value");
        }

        #endregion
    }
}