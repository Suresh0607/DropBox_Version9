using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.DropBox
{
    public class DropBox_TestIter
    {
        public IWebDriver driver;

        public DropBox_TestIter(String BrowserType) 
        {
            CommonTestExecute cte = new CommonTestExecute();
		    this.driver = cte.SetUp(BrowserType);		    
	    }
    }
}
