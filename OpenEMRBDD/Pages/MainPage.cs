using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenEMRBDD.Pages
{
    class MainPage
    {
        private By patientClientLocator = By.XPath("//div[text()='Patient/Client']");

        private IWebDriver driver;
        public MainPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ClickOnPatientClient()
        {
            driver.FindElement(patientClientLocator).Click();
        }
    }
}
