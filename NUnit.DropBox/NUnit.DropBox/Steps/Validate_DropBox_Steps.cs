using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using BoDi;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
//using NUnit.InsightsRetail.CommonClasses;
//using NUnit.InsightsRetail.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using TechTalk.SpecFlow;
using Unity;

namespace NUnit.DropBox.Steps
{
    [Binding]
    public class Validate_DropBox_Steps
    {
        private IWebDriver _driver;        
        Login_Page login_Page;
        Home_Page home_page;
        File_Page file_page;
       
        public Validate_DropBox_Steps(IWebDriver driver)
        {
            this._driver = driver;
        }
        

        [Given(@"user (.*) with (.*) logs into the (.*)")]
        public void GivenUserWithLogsIntoThe(string userName, string password, string url)
        {
            this.login_Page = new Login_Page(_driver);
            this.login_Page.gotologinPage(userName, password, url);
        }
                     
        [Then(@"user logout")]
        public void User_Logout()
        {
            _driver.FindElement(By.XPath("/html/body/div/div[1]/div[1]/a[3]")).Click();          
        }
               
        [Then(@"user NaviateTo '(.*)'")]
        public void ThenUserNaviateTo(string Menu)
        {
            switch (Menu)
            {
                case "Files":
                    this.home_page = new Home_Page(_driver);
                    home_page.goTo_FilesPage();
                    break;
                case "Paper":
                    Console.WriteLine("Case 2");
                    break;               
            }
        }
                      
        [Then(@"user creates NewFolder (.*)")]
        public void ThenUserCreatesNewFolder(string folderName)
        {
            file_page = new File_Page(_driver);
            file_page.createNewFolder(folderName);
        }
 
        [Then(@"user upload files (.*) to (.*)")]
        public void ThenUserUploadFilesTo(string files, string folderName)
        {
            file_page = new File_Page(_driver);
            file_page.uploadFiles(files,folderName);
        }

        [Then(@"user can share (.*) to (.*)")]
        public void ThenUserCanShareTo(string folderName, string eMailID)
        {
            file_page = new File_Page(_driver);
            file_page.shareFolder(folderName, eMailID);
        }

        [Then(@"user delete the (.*)")]
        public void ThenUserDeleteThe(string folderName)
        {
            file_page = new File_Page(_driver);
            file_page.deleteFolder(folderName);
        }
        
        [Then(@"user Logs out of the application")]
        public void ThenUserLogsOutOfTheApplication()
        {
            login_Page = new Login_Page(_driver);
            login_Page.signOut();
        }

        [Then(@"I should see the '(.*)' as PageHeader")]
        public void ThenIShouldSeeTheAsPageHeader(string pageTitle)
        {
            home_page = new Home_Page(_driver);
            home_page.verifyPageHeader(pageTitle);
        }

        [Then(@"I should see the '(.*)' as PageTitle")]
        public void ThenIShouldSeeTheAsPageTitle(string pageTitle)
        {
            file_page = new File_Page(_driver);
            file_page.verifyPageTitle(pageTitle);
        }

        [Then(@"user should see the newly create Folder (.*)")]
        public void ThenUserShouldSeeTheNewlyCreateFolderTestFolder(string folder)
        {
            file_page = new File_Page(_driver);
            file_page.veriyFolderExists(folder,true);
        }

        [Then(@"verify newly Upload files (.*) exists under the folder (.*)")]
        public void ThenVerifyNewlyUploadFiles_Under_TestFolder(string files, string folderName)
        {
            file_page = new File_Page(_driver);
            file_page.veriyFileExists(files, folderName);
        }

        [Then(@"verify folder (.*) is shared to (.*)")]
        public void Then_Verify_Folder_is_SharedTo(string folder, string sharedUser)
        {
            file_page = new File_Page(_driver);
            file_page.verifyFolderisSharedTo(folder,sharedUser);
        }

        [Then(@"verify folder (.*) is deleted")]
        public void Then_Verify_Folder_IsDeleted(string folderName)
        {
            file_page = new File_Page(_driver);
            file_page.veriyFolderExists(folderName,false);
        }


    }
}
