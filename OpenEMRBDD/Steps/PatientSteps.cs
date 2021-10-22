using OpenEMRBDD.Hooks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace OpenEMRBDD.Steps
{
    [Binding]
    public class PatientSteps 
    {
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
            Console.WriteLine(table.RowCount);
            Console.WriteLine(table.Rows[0][0]);

            Console.WriteLine(table.Rows[0]["firstname"]);

            Console.WriteLine(table.Rows[0]["gender"]);
        }

        [When(@"I click on create new patient")]
        public void WhenIClickOnCreateNewPatient()
        {
            
        }

    }
}
