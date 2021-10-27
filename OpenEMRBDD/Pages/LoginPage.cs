using OpenEMRBDD.Hooks;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenEMRBDD.Pages
{
    class LoginPage
    {
        private static By usernameLocator = By.Id("authUser");
        public static void EnterUsername(string username)
        {
            AutomationHooks.driver.FindElement(usernameLocator).SendKeys(username);
        }
    }
}
