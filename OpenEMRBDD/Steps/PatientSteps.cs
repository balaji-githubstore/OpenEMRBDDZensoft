using NUnit.Framework;
using OpenEMRBDD.Hooks;
using OpenEMRBDD.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;

namespace OpenEMRBDD.Steps
{
    [Binding]
    public class PatientSteps 
    {
        private ScenarioContext scenarioContext;
        private AutomationHooks hooks;
        private MainPage _mainPage;
        private PatientFinderPage _patientFinderPage;

        public PatientSteps(ScenarioContext scenarioContext, AutomationHooks hooks)
        {
            this.scenarioContext = scenarioContext;
            this.hooks = hooks;
            InitializePages();
        }
        public void InitializePages()
        {
            _mainPage = new MainPage(hooks.driver);
            _patientFinderPage = new PatientFinderPage(hooks.driver);
            //MainPage
            //PatientFinderPage
            //SearchOrAddPatientPage
            //PatientDashboardPage
        }



        [When(@"I click on patient-client")]
        public void WhenIClickOnPatient_Client()
        {
            _mainPage.ClickOnPatientClient();
        }
        
        [When(@"I click on patients")]
        public void WhenIClickOnPatients()
        {
            hooks.driver.FindElement(By.XPath("//div[text()='Patients']")).Click();
        }
        
        [When(@"I click on add new patient")]
        public void WhenIClickOnAddNewPatient()
        {
            hooks.driver.SwitchTo().Frame("fin");
            hooks.driver.FindElement(By.Id("create_patient_btn1")).Click();
            hooks.driver.SwitchTo().DefaultContent();
        }
        [When(@"I fill the patient detail")]
        public void WhenIFillThePatientDetail(Table table)
        {

            hooks.driver.SwitchTo().Frame(hooks.driver.FindElement(By.XPath("//iframe[@name='pat']")));

            string firstname = table.Rows[0]["firstname"];
            hooks.driver.FindElement(By.Id("form_fname")).SendKeys(firstname);
            hooks.driver.FindElement(By.Id("form_lname")).SendKeys(table.Rows[0]["lastname"]);
            hooks.driver.FindElement(By.Id("form_DOB")).SendKeys(table.Rows[0]["dob"]);

            hooks.driver.FindElement(By.Id("form_drivers_license")).SendKeys(table.Rows[0]["licensenumber"]);

            SelectElement selectGender = new SelectElement(hooks.driver.FindElement(By.Id("form_sex")));
            selectGender.SelectByText(table.Rows[0]["gender"]);
            hooks.driver.SwitchTo().DefaultContent();


        }

        [When(@"I click on create new patient")]
        public void WhenIClickOnCreateNewPatient()
        {
            hooks.driver.SwitchTo().Frame(hooks.driver.FindElement(By.XPath("//iframe[@name='pat']")));
            hooks.driver.FindElement(By.Id("create")).Click();
            hooks.driver.SwitchTo().DefaultContent();
        }

        [When(@"I click on confirm create new patient")]
        public void WhenIClickOnConfirmCreateNewPatient()
        {
            hooks.driver.SwitchTo().Frame(hooks.driver.FindElement(By.XPath("//iframe[@id='modalframe']")));
            hooks.driver.FindElement(By.XPath("//input[@value='Confirm Create New Patient']")).Click();
            hooks.driver.SwitchTo().DefaultContent();
        }

        [When(@"I store the text and handle the alert")]
        public void WhenIStoreTheTextAndHandleTheAlert()
        {
            WebDriverWait wait = new WebDriverWait(hooks.driver, TimeSpan.FromSeconds(50));
            wait.Until(x=>x.SwitchTo().Alert());

            string actualAlertText = hooks.driver.SwitchTo().Alert().Text;

            //keeping the key alerttext in the current scenario memory
            scenarioContext.Add("alerttext", actualAlertText);


            hooks.driver.SwitchTo().Alert().Accept();
        }

        [When(@"I close the happy birthday popup if dob is today's date")]
        public void WhenICloseTheHappyBirthdayPopupIfDobIsTodaySDate()
        {
            if(hooks.driver.FindElements(By.XPath("//div[@data-dismiss='modal']")).Count>0)
            {
                hooks.driver.FindElement(By.XPath("//div[@data-dismiss='modal']")).Click();
            }
        }

        [Then(@"I should get the alert message as '(.*)'")]
        public void ThenIShouldGetTheAlertMessageAs(string expectedAlertText)
        {
            //if(scenarioContext.TryGetValue("myname",out string name))
            //{
            //    Console.WriteLine(name);
            //}

            //if (scenarioContext.TryGetValue("currentTeam", out string name1))
            //{
            //    Console.WriteLine(name1);
            //}

            if(scenarioContext.TryGetValue("alerttext",out string actualAlert))
            {
                Assert.IsTrue(actualAlert.Contains(expectedAlertText)); // failure on false 
            }     
        }

        [Then(@"I should get the added patient detail as '(.*)'")]
        public void ThenIShouldGetTheAddedPatientDetailAs(string expectedValue)
        {
            hooks.driver.SwitchTo().Frame(hooks.driver.FindElement(By.XPath("//iframe[@name='pat']")));

            string actualValue = hooks.driver.FindElement(By.XPath("//*[contains(text(),'Medical Record')]")).Text.Trim();

            Assert.AreEqual(expectedValue,actualValue);
        }

    }
}
