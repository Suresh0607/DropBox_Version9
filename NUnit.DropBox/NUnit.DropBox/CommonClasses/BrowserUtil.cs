using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.DropBox
{
    public class BrowserUtil
    {
        private IWebDriver driver;

        public BrowserUtil(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void mouseOverClick( IWebDriver driver, IWebElement webObj)
        {

            String strJavaScript = "var element = arguments[0];"
                    + "var mouseEventObj = document.createEvent('MouseEvents');"
                    + "mouseEventObj.initEvent( 'mouseover', true, true );"
                    + "element.dispatchEvent(mouseEventObj);";

            ((IJavaScriptExecutor)driver).ExecuteScript(strJavaScript, webObj);

            webObj.Click();
        }        
    }
}
