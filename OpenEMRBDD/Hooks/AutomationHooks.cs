using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
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

        public static ExtentReports extent;
        private static ExtentTest feature;
        private static ExtentTest scenario;
        private static string featureTitle;

        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;

        public AutomationHooks(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }

        public void LaunchBrowser(string browser="ch")
        {
            if(browser.Equals("ff"))
            {
                driver = new FirefoxDriver();
            }
            else
            {
                driver = new ChromeDriver();
            }
            
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Url = "https://demo.openemr.io/b/openemr";
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            string reportPath = @"D:\BDDExtent.html"; //where to save
            var report = new ExtentSparkReporter(reportPath);

            extent = new ExtentReports();
            extent.AttachReporter(report); //accumulate the html while running the scenarios
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            extent.Flush(); //generate html with accumulated html
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            if (featureTitle != featureContext.FeatureInfo.Title)
            {
                feature = extent.CreateTest(new GherkinKeyword("Feature"), "Feature: " + featureContext.FeatureInfo.Title);
            }
            featureTitle = featureContext.FeatureInfo.Title;
            scenario = feature.CreateNode(new GherkinKeyword("Scenario"), "Scenario: " + scenarioContext.ScenarioInfo.Title);
        }

        //runs after each scenario
        [AfterScenario]
        public void EndScenario()
        {
            driver.Quit();
        }

        [AfterStep]
        public void AfterStep()
        {
            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            if (scenarioContext.TestError == null)
            {
                scenario.CreateNode(new GherkinKeyword(stepType), scenarioContext.StepContext.StepInfo.Text);
            }
            else if (scenarioContext.TestError != null)
            {
                scenario.CreateNode(new GherkinKeyword(stepType), scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
            }
        }
    }
}
