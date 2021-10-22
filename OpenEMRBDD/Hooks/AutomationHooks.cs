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

        private FeatureContext featureContext;
        private ScenarioContext scenarioContext;

        public AutomationHooks(FeatureContext featureContext,ScenarioContext scenarioContext)
        {
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;
        }

        //runs after each scenario
        [AfterScenario]
        public void EndScenario()
        {
            string scenarioName = scenarioContext.ScenarioInfo.Title;
            Console.WriteLine(scenarioName);
            driver.Quit();
        }

    }
}
