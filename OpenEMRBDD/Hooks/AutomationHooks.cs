using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace OpenEMRBDD.Hooks
{
    [Binding]
    public class AutomationHooks
    {
        public static IWebDriver driver;


        [AfterScenario]
        public void EndScenario()
        {
            driver.Quit();
        }

    }
}
