using NUnit.Framework;
using OpenEMRBDD.Hooks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;

namespace OpenEMRBDD.Steps
{
    [Binding]
    public class LoginSteps
    {

        private FeatureContext featureContext;
        private ScenarioContext scenarioContext;

        public LoginSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;
        }






        [Given(@"I have browser with OpenEmr url")]
        public void GivenIHaveBrowserWithOpenemrUrl()
        {
            scenarioContext.Add("myname", "balaji");
            scenarioContext.Add("currentTeam", "zensoft");

            AutomationHooks.driver = new ChromeDriver();
            AutomationHooks.driver.Manage().Window.Maximize();
            AutomationHooks.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            AutomationHooks.driver.Url = "https://demo.openemr.io/b/openemr";
        }

        [Given(@"I enter username as '(.*)'")]
        [When(@"I enter username as '(.*)'")]
        public void WhenIEnterUsernameAs(string username)
        {
            AutomationHooks.driver.FindElement(By.Id("authUser")).SendKeys(username);
        }

        [When(@"I enter password as '(.*)'")]
        public void WhenIEnterPasswordAs(string password)
        {
            AutomationHooks.driver.FindElement(By.Id("clearPass")).SendKeys(password);
        }

        [When(@"I select language as '(.*)'")]
        public void WhenISelectLanguageAs(string language)
        {
            SelectElement selectLanuage = new SelectElement(AutomationHooks.driver.FindElement(By.Name("languageChoice")));
            selectLanuage.SelectByText(language);
        }

        [When(@"I click on login")]
        public void WhenIClickOnLogin()
        {
            AutomationHooks.driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        [Then(@"I should get access to the dashboard with title as '(.*)'")]
        public void ThenIShouldGetAccessToTheDashboardWithTitleAs(string expectedValue)
        {
            WebDriverWait wait = new WebDriverWait(AutomationHooks.driver, TimeSpan.FromSeconds(50));
            wait.Until(x => x.FindElement(By.XPath("//*[text()='About']")));

            string actualValue = AutomationHooks.driver.Title;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Then(@"I should get the error stating '(.*)'")]
        public void ThenIShouldGetTheErrorStating(string expectedError)
        {
            string actualValue = AutomationHooks.driver.FindElement(By.XPath("//*[contains(text(),'Invalid')]")).Text.Trim();
            Assert.AreEqual(expectedError, actualValue);
        }


        [Then(@"I should get access to the dashboard with text '(.*)' and title as '(.*)'")]
        public void ThenIShouldGetAccessToTheDashboardWithTextTitleAs(string waitForText, string expectedValue)
        {
            WebDriverWait wait = new WebDriverWait(AutomationHooks.driver, TimeSpan.FromSeconds(50));
            wait.Until(x => x.FindElement(By.XPath("//*[text()='"+waitForText+"']")));

            string actualValue = AutomationHooks.driver.Title;

            Assert.AreEqual(expectedValue, actualValue);
        }


    }
}
