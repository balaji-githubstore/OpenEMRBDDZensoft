using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace OpenEMRBDD.Hooks
{
    [Binding]
    public class AutomationHooks
    {
        public IWebDriver driver;

        public void LaunchBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Url = "https://demo.openemr.io/b/openemr";
        }


        //runs after each scenario
        [AfterScenario]
        public void EndScenario()
        {
            driver.Quit();
        }


     
    }
}
