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
    public class Login_Page
    {
        private IWebDriver LoginDriver;

        public Login_Page(IWebDriver driver)
        {
            this.LoginDriver = driver;
            
        }

        public void gotologinPage(string userName, string password, string url)
        {
            try
            {
                LoginDriver.Navigate().GoToUrl(url);
                new WebDriverWait(LoginDriver, TimeSpan.FromSeconds(500000)).Until(
                 d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
                LoginDriver.Manage().Window.Maximize();
                LoginDriver.FindElement(By.XPath("//*[@name='login_email']")).SendKeys(userName);
                LoginDriver.FindElement(By.XPath("//*[@name='login_password']")).SendKeys(password);
                LoginDriver.FindElement(By.XPath("//*[@class='login-button signin-button button-primary']")).Click();

                //new WebDriverWait(LoginDriver, TimeSpan.FromSeconds(500000)).Until(
                //d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
                Thread.Sleep(25000);
                IWebElement HomeLink =LoginDriver.FindElement(By.XPath("//*[@id='home']"));
                Assert.AreEqual(true, HomeLink.Displayed);
            }
            catch(Exception e)
            {
                throw (e);
            }
            
        }

        public void signOut()
        {
            try
            {
                IWebElement avatarBtn  = LoginDriver.FindElement(By.XPath("//*[@id='maestro-header']/div/div[2]/div/span"));
                avatarBtn.Click();


                Thread.Sleep(3000);

                IWebElement signOutBtn = LoginDriver.FindElement(By.XPath("//a[@href='https://www.dropbox.com/logout']"));
                signOutBtn.Click();
            }
            catch (Exception e)
            {
                throw (e);
            }

        }
        
    }
}
