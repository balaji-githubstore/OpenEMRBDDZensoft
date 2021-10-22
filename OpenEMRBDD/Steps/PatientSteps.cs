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
    public class PatientSteps 
    {
        private FeatureContext featureContext;
        private ScenarioContext scenarioContext;

        public PatientSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;
        }
        //private static string actualAlertText;


        [When(@"I click on patient-client")]
        public void WhenIClickOnPatient_Client()
        {
            AutomationHooks.driver.FindElement(By.XPath("//div[text()='Patient/Client']")).Click();
        }
        
        [When(@"I click on patients")]
        public void WhenIClickOnPatients()
        {
            AutomationHooks.driver.FindElement(By.XPath("//div[text()='Patients']")).Click();
        }
        
        [When(@"I click on add new patient")]
        public void WhenIClickOnAddNewPatient()
        {
            AutomationHooks.driver.SwitchTo().Frame("fin");
            AutomationHooks.driver.FindElement(By.Id("create_patient_btn1")).Click();
            AutomationHooks.driver.SwitchTo().DefaultContent();
        }
        [When(@"I fill the patient detail")]
        public void WhenIFillThePatientDetail(Table table)
        {

            AutomationHooks.driver.SwitchTo().Frame(AutomationHooks.driver.FindElement(By.XPath("//iframe[@name='pat']")));

            string firstname = table.Rows[0]["firstname"];
            AutomationHooks.driver.FindElement(By.Id("form_fname")).SendKeys(firstname);
            AutomationHooks.driver.FindElement(By.Id("form_lname")).SendKeys(table.Rows[0]["lastname"]);
            AutomationHooks.driver.FindElement(By.Id("form_DOB")).SendKeys(table.Rows[0]["dob"]);

            AutomationHooks.driver.FindElement(By.Id("form_drivers_license")).SendKeys(table.Rows[0]["licensenumber"]);

            SelectElement selectGender = new SelectElement(AutomationHooks.driver.FindElement(By.Id("form_sex")));
            selectGender.SelectByText(table.Rows[0]["gender"]);
            AutomationHooks.driver.SwitchTo().DefaultContent();


        }

        [When(@"I click on create new patient")]
        public void WhenIClickOnCreateNewPatient()
        {
            AutomationHooks.driver.SwitchTo().Frame(AutomationHooks.driver.FindElement(By.XPath("//iframe[@name='pat']")));
            AutomationHooks.driver.FindElement(By.Id("create")).Click();
            AutomationHooks.driver.SwitchTo().DefaultContent();
        }

        [When(@"I click on confirm create new patient")]
        public void WhenIClickOnConfirmCreateNewPatient()
        {
            AutomationHooks.driver.SwitchTo().Frame(AutomationHooks.driver.FindElement(By.XPath("//iframe[@id='modalframe']")));
            AutomationHooks.driver.FindElement(By.XPath("//input[@value='Confirm Create New Patient']")).Click();
            AutomationHooks.driver.SwitchTo().DefaultContent();
        }

        [When(@"I store the text and handle the alert")]
        public void WhenIStoreTheTextAndHandleTheAlert()
        {
            WebDriverWait wait = new WebDriverWait(AutomationHooks.driver, TimeSpan.FromSeconds(50));
            wait.Until(x=>x.SwitchTo().Alert());

            string actualAlertText = AutomationHooks.driver.SwitchTo().Alert().Text;

            //keeping the key alerttext in the current scenario memory
            scenarioContext.Add("alerttext", actualAlertText);


            AutomationHooks.driver.SwitchTo().Alert().Accept();
        }

        [When(@"I close the happy birthday popup if dob is today's date")]
        public void WhenICloseTheHappyBirthdayPopupIfDobIsTodaySDate()
        {
            if(AutomationHooks.driver.FindElements(By.XPath("//div[@data-dismiss='modal']")).Count>0)
            {
                AutomationHooks.driver.FindElement(By.XPath("//div[@data-dismiss='modal']")).Click();
            }
        }

        [Then(@"I should get the alert message as '(.*)'")]
        public void ThenIShouldGetTheAlertMessageAs(string expectedAlertText)
        {
            if(scenarioContext.TryGetValue("myname",out string name))
            {
                Console.WriteLine(name);
            }

            if (scenarioContext.TryGetValue("currentTeam", out string name1))
            {
                Console.WriteLine(name1);
            }

            if(scenarioContext.TryGetValue("alerttext",out string actualAlert))
            {
                Assert.IsTrue(actualAlert.Contains(expectedAlertText)); // failure on false 
            }     
        }

        [Then(@"I should get the added patient detail as '(.*)'")]
        public void ThenIShouldGetTheAddedPatientDetailAs(string expectedValue)
        {
            AutomationHooks.driver.SwitchTo().Frame(AutomationHooks.driver.FindElement(By.XPath("//iframe[@name='pat']")));

            string actualValue = AutomationHooks.driver.FindElement(By.XPath("//*[contains(text(),'Medical Record')]")).Text.Trim();

            Assert.AreEqual(expectedValue,actualValue);
        }

    }
}
