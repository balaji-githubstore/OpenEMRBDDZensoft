using NUnit.Framework;
using OpenEMRBDD.Hooks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;
using Zensoft.OpenErmApplication.Pages;

namespace OpenEMRBDD.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly AutomationHooks hooks;
        private LoginPage _loginPage;

        public LoginSteps(AutomationHooks hooks)
        { 
            this.hooks = hooks;
        }

        public void InitializePages()
        {
            _loginPage = new LoginPage(hooks.driver);
        }

        [Given(@"I have browser with OpenEmr url")]
        public void GivenIHaveBrowserWithOpenemrUrl()
        {
            //scenarioContext.Add("myname", "balaji");
            //scenarioContext.Add("currentTeam", "zensoft");
            hooks.LaunchBrowser();
            InitializePages();
        }

        [Given(@"I enter username as '(.*)'")]
        [When(@"I enter username as '(.*)'")]
        public void WhenIEnterUsernameAs(string username)
        {
            _loginPage.EnterUsername(username);
        }

        [When(@"I enter password as '(.*)'")]
        public void WhenIEnterPasswordAs(string password)
        {
            _loginPage.EnterPassword(password);
            
        }

        [When(@"I select language as '(.*)'")]
        public void WhenISelectLanguageAs(string language)
        {
            _loginPage.SelectLanguageByText(language);
        }

        [When(@"I click on login")]
        public void WhenIClickOnLogin()
        {
            _loginPage.ClickOnLogin();
        }

        [Then(@"I should get access to the dashboard with title as '(.*)'")]
        public void ThenIShouldGetAccessToTheDashboardWithTitleAs(string expectedValue)
        {
            WebDriverWait wait = new WebDriverWait(hooks.driver, TimeSpan.FromSeconds(50));
            wait.Until(x => x.FindElement(By.XPath("//*[text()='About']")));

            string actualValue = hooks.driver.Title;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Then(@"I should get the error stating '(.*)'")]
        public void ThenIShouldGetTheErrorStating(string expectedError)
        {

            string actualValue = _loginPage.GetInvalidErrorMessage();
            Assert.AreEqual(expectedError, actualValue);
        }


        [Then(@"I should get access to the dashboard with text '(.*)' and title as '(.*)'")]
        public void ThenIShouldGetAccessToTheDashboardWithTextTitleAs(string waitForText, string expectedValue)
        {
            WebDriverWait wait = new WebDriverWait(hooks.driver, TimeSpan.FromSeconds(50));
            wait.Until(x => x.FindElement(By.XPath("//*[text()='"+waitForText+"']")));

            string actualValue = hooks.driver.Title;

            Assert.AreEqual(expectedValue, actualValue);
        }


    }
}
