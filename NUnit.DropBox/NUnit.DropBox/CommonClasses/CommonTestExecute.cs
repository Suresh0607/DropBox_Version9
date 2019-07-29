using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using System.Runtime.CompilerServices;

namespace NUnit.DropBox
{
    public class CommonTestExecute
    {
        protected IWebDriver Driver;
        protected ICapabilities Capabilities;
        protected string TargetUrl;
        protected string GridUrl;      

        public IWebDriver SetUp(String BrowserType)
        {
            RemoteWebDriverSetup threadDriverRemote = new RemoteWebDriverSetup();
            Driver = threadDriverRemote.LaunchBrowser(BrowserType);
            return Driver;
        }
    }
 }
