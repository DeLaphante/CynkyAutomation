using CynkyAutomation.Models.UI;
using CynkyAutomation.PageObjects.CommonPages;
using CynkyDriver;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace CynkyAutomation.PageObjects.OrangeHRM
{
    public class PIMPage : Navigation
    {
        public PIMPage(IWebDriver driver) : base(driver)
        {
            _Driver = driver;
        }

        #region Locators

        PageElement DeleteUser_button(string firstname) => new PageElement(_Driver, By.XPath($"//div[contains(normalize-space(),'{firstname}') and contains(@class,'row')]//button[i[contains(@class,'trash')]]"));
        PageElement Row_label(string textNotOnColumn, int index) => new PageElement(_Driver, By.XPath($"(//div[@class='oxd-table-card' and not(contains(.,'{textNotOnColumn}'))])[{index}]//div[3]"));
        PageElement EmployeeRow_label => new PageElement(_Driver, By.XPath($"//div[@class='oxd-table-card']"));
        PageElement FormInput_textbox(string name) => new PageElement(_Driver, By.XPath($"//input[@name=\"{name}\"]"));

        #endregion

        #region Actions

        public void DeleteUser(string firstname)
        {
            DeleteUser_button(firstname).Click();
            ClickButton("Yes, Delete");
        }

        public bool IsUserPresent(string firstname)
        {
            return DeleteUser_button(firstname).ElementExists();
        }

        public string GetFirstNameOfEmployeeWithoutEmploymentStatus(string status)
        {
            int i = 1;
            while (Row_label(status, i).GetText().Length < 3 && i < 100)
            {
                i++;
            }
            return Row_label(status, i).GetText();
        }

        public void AddEmployee(EmployeeProfile employeeProfile)
        {
            ClickButton("Add");
            FormInput_textbox("firstName").SendKeys(employeeProfile.Firstname);
            FormInput_textbox("lastName").SendKeys(employeeProfile.Lastname);
            ClickButton("Save");
        }

        public List<string> GetAllEmployees()
        {
            List<string> employees = new List<string>();
            EmployeeRow_label.IsDisplayed();
            var employeesList = EmployeeRow_label.GetAllElements();
            foreach (var item in employeesList)
            {
                employees.Add(item.GetText());
            }
            return employees;
        }

        #endregion
    }
}