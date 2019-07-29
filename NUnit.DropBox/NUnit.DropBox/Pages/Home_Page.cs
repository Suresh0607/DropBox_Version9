using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NUnit.DropBox
{
    public class Home_Page
    {
        private IWebDriver HomeDriver;

        public Home_Page(IWebDriver driver)
        {
            this.HomeDriver = driver;           
        }

        Dictionary<string, string> dicRepo = new Dictionary<string, string>()
        {
            { "FilesLink"              , "//*[@id='files']"}
        };

        public void goTo_FilesPage()
        {
            HomeDriver.FindElement(By.XPath(dicRepo["FilesLink"])).Click();
        }

        public void clickon(string objectName)
        {
            Thread.Sleep(200);
            HomeDriver.FindElement(By.XPath(dicRepo[objectName])).Click();
        }

        public void verifyPageHeader(string pageHeader)
        {
            string _pageHeader = HomeDriver.FindElement(By.XPath(".//h1[@class='page-header__heading']")).Text;
            Assert.AreEqual(_pageHeader, pageHeader);
        }
    }
}
