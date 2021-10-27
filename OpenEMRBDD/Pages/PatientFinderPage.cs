using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenEMRBDD.Pages
{
    class PatientFinderPage
    {
        private IWebDriver driver;
        public PatientFinderPage(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
