using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Threading;
using System.Drawing;
using OpenQA.Selenium.Interactions;
using System.IO;

namespace NUnit.DropBox
{

    public class RemoteWebDriverSetup 
    {
        private ChromeOptions options;
        private RemoteWebDriver _driver;

        public IWebDriver LaunchBrowser(String BrowserType)
        {
            if(BrowserType.Equals("chrome"))
            {
                options = new ChromeOptions();
                _driver = new ChromeDriver(@"C:\git\DropBox_V3\DropBox_V_10\NUnit.DropBox\ChromeDriver\", options); //<-Add your path
            }
            return _driver;
        }
    }
}
