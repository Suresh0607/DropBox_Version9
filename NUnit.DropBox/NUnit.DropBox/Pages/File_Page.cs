using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NUnit.DropBox
{
    public class File_Page
    {
        private IWebDriver FileDriver;
        private BrowserUtil bu;

        public File_Page(IWebDriver driver)
        {
            this.FileDriver = driver;
            this.bu = new BrowserUtil(FileDriver);
        }

        Dictionary<string, string> dicRepo = new Dictionary<string, string>()
        {
            { "FilesLink"                   , "//*[@id='files']"},
            { "CreateFolderModal"           , "//*[@id='cdm-create-folder-modal']"},
            { "NewFolderName"               , ".//input[@class='cdm-create-folder-modal__content-name mc-input']" },
            { "OnlyYouCanAccessFolder"      , ".//input[@id='not_confidential_option']"},
            { "CreateButton"                , ".//button[@class='button-primary dbmodal-button']" }
        };

        public void clickon(string objectName)
        {
            Thread.Sleep(200);
            FileDriver.FindElement(By.XPath(dicRepo[objectName])).Click();
        }

        public void createNewFolder(string folderName)
        {
            Thread.Sleep(10000);
            FileDriver.FindElement(By.XPath(".//div[@class='ue-effect-container uee-AppActionsView-SecondaryActionMenu-text-new-folder']")).Click();
            Thread.Sleep(20000);
            IWebElement crtFolderModal = FileDriver.FindElement(By.XPath(dicRepo["CreateFolderModal"]));
            crtFolderModal.FindElement(By.XPath(dicRepo["NewFolderName"])).SendKeys(folderName);
            crtFolderModal.FindElement(By.XPath(dicRepo["OnlyYouCanAccessFolder"])).Click();
            crtFolderModal.FindElement(By.XPath(dicRepo["CreateButton"])).Click();

        }

        public void uploadFiles(string files,string folderName)
        {
            string[] _files = files.Split(':');
            foreach (string _file in _files)
            {
                selectFolder(folderName);
                Thread.Sleep(30000);
                FileDriver.FindElement(By.XPath(".//div[@class='ue-effect-container uee-AppActionsView-SecondaryActionMenu-text-upload-file']")).Click();
                Thread.Sleep(10000);
                SendKeys.SendWait(@"C:\git\DropBox_V3\DropBox_V_10\NUnit.DropBox\TestData\FilesToUpload\" + _file);
                Thread.Sleep(10000);
                SendKeys.SendWait(@"{Enter}");
                Thread.Sleep(10000);
            }
            
        }

        public void shareFolder(string folderName, string eMailID)
        {
            IWebElement _desiredFolderRow = findFolder(folderName);
            IWebElement _folderRowCheckBox = _desiredFolderRow.FindElement(By.XPath(".//span[@class='mc-checkbox-border']"));
            //_folderRowCheckBox.Click();
            //bu.mouseOverClick(FileDriver, _folderRowCheckBox,true);

            String strJavaScript = "var element = arguments[0];"
                    + "var mouseEventObj = document.createEvent('MouseEvents');"
                    + "mouseEventObj.initEvent( 'mouseover', true, true );"
                    + "element.dispatchEvent(mouseEventObj);";

            ////Then JavascriptExecutor class is used to execute the script to trigger the dispatched event.
            ((IJavaScriptExecutor)FileDriver).ExecuteScript(strJavaScript, _folderRowCheckBox);

            _folderRowCheckBox.Click();

            Thread.Sleep(20000);
            IWebElement shareButton = FileDriver.FindElement(By.XPath(".//button[@class='primary-action-menu__button action-share mc-button mc-button-primary']"));
            shareButton.Click();
            Thread.Sleep(20000);
            IWebElement eMailId = FileDriver.FindElement(By.XPath(".//input[@class='mc-tokenized-input-input']"));
            eMailId.SendKeys(eMailID);
            IWebElement _shareButton = FileDriver.FindElement(By.XPath(".//button[@class='scl-sharing-modal-footer-inband__button mc-button mc-button-primary']"));

            String _strJavaScript = "var element = arguments[0];"
                    + "var mouseEventObj = document.createEvent('MouseEvents');"
                    + "mouseEventObj.initEvent( 'mouseover', true, true );"
                    + "element.dispatchEvent(mouseEventObj);";

            ////Then JavascriptExecutor class is used to execute the script to trigger the dispatched event.
            ((IJavaScriptExecutor)FileDriver).ExecuteScript(_strJavaScript, _shareButton);



            _shareButton.Click();
            Thread.Sleep(20000);

        }

        public void selectFolder(string _foldrName)
        {
            IWebElement myFiles = FileDriver.FindElement(By.XPath(".//span[@class='ue-effect-container uee-FeatureNav-myFiles']/a"));
            myFiles.Click();

            Thread.Sleep(10000);

            IWebElement filesView = FileDriver.FindElement(By.XPath(".//div[@class='brws-files-view brws-files-view--list_view']"));
            IWebElement tblfileView = filesView.FindElement(By.XPath(".//tbody[@class='mc-table-body mc-table-body-culled']"));

            IList<IWebElement> _folders = tblfileView.FindElements(By.TagName("tr"));

            foreach (IWebElement _folder in _folders)
            {

                IWebElement _folderLink = _folder.FindElement(By.XPath(".//a[@class='brws-file-name-cell-filename']"));
                String _folderName = _folder.FindElement(By.XPath(".//a[@class='brws-file-name-cell-filename']/div/span")).Text;
                if (_folderName.Equals(_foldrName))
                {
                    _folderLink.Click();
                    break;
                }
            }
        }

        public IWebElement findFolder(string _foldrName)
        {
            IWebElement desiredFolder = null;
            IWebElement myFiles = FileDriver.FindElement(By.XPath(".//span[@class='ue-effect-container uee-FeatureNav-myFiles']/a"));
            myFiles.Click();

            Thread.Sleep(10000);

            IWebElement filesView = FileDriver.FindElement(By.XPath(".//div[@class='brws-files-view brws-files-view--list_view']"));
            IWebElement tblfileView = filesView.FindElement(By.XPath(".//tbody[@class='mc-table-body mc-table-body-culled']"));

            IList<IWebElement> _folders = tblfileView.FindElements(By.TagName("tr"));

            foreach (IWebElement _folder in _folders)
            {

                IWebElement _folderLink = _folder.FindElement(By.XPath(".//a[@class='brws-file-name-cell-filename']"));
                String _folderName = _folder.FindElement(By.XPath(".//a[@class='brws-file-name-cell-filename']/div/span")).Text;
                if (_folderName.Equals(_foldrName))
                {
                    desiredFolder = _folder;
                    break;
                }
            }
            return desiredFolder;
        }

        public void deleteFolder(string folderName)
        {
            IWebElement _desiredFolderRow = findFolder(folderName);
            IWebElement _folderRowCheckBox = _desiredFolderRow.FindElement(By.XPath(".//span[@class='mc-checkbox-border']"));
           
            String strJavaScript = "var element = arguments[0];"
                    + "var mouseEventObj = document.createEvent('MouseEvents');"
                    + "mouseEventObj.initEvent( 'mouseover', true, true );"
                    + "element.dispatchEvent(mouseEventObj);";

            ((IJavaScriptExecutor)FileDriver).ExecuteScript(strJavaScript, _folderRowCheckBox);

            _folderRowCheckBox.Click();

            Thread.Sleep(20000);
            IWebElement deleteButton = FileDriver.FindElement(By.XPath(".//button[@class='mc-tertiary-link-button secondary-action-menu__button action-delete']"));
            //deleteButton.Click();
            
            String _strJavaScript = "var element = arguments[0];"
                    + "var mouseEventObj = document.createEvent('MouseEvents');"
                    + "mouseEventObj.initEvent( 'mouseover', true, true );"
                    + "element.dispatchEvent(mouseEventObj);";

            ((IJavaScriptExecutor)FileDriver).ExecuteScript(_strJavaScript, deleteButton);

            deleteButton.Click();
            Thread.Sleep(20000);

            IWebElement removeShareFolder = FileDriver.FindElement(By.XPath(".//div[@class='ReactModal__Content ReactModal__Content--after-open mc-modal']"));
            IWebElement removeBtn = removeShareFolder.FindElement(By.XPath(".//button[@class='mc-button mc-button-primary']"));

            removeBtn.Click();
        }

        public void verifyPageTitle(string pageTitle)
        {
            string _pageTitle = FileDriver.FindElement(By.XPath(".//span[@class='ue-effect-container uee-PathBreadcrumbs-HomeTitle']")).Text;
            Assert.AreEqual(_pageTitle, pageTitle);
        }

        public void veriyFolderExists(string folder, Boolean ieExists)
        {
            Boolean _ieExists = false;
            try
            {
                IWebElement flder = findFolder(folder);
                String _folderName = flder.FindElement(By.XPath(".//a[@class='brws-file-name-cell-filename']/div/span")).Text;
                if (_folderName.Equals(folder))
                {
                    _ieExists = true;
                }
            }catch(Exception e)
            {
                 _ieExists = false;
            }
            Assert.AreEqual(ieExists, _ieExists);
        }

        public void veriyFileExists(string flNames, string folder)
        {
            selectFolder(folder);
            Thread.Sleep(30000);

            string[] fileNames = flNames.Split(':');
            foreach (string fileName in fileNames)
            {
                IWebElement tblFileViewList = FileDriver.FindElement(By.XPath(".//table[@class='mc-table brws-files-view-list']/tbody"));
                IList<IWebElement> filesList = tblFileViewList.FindElements(By.TagName("tr"));
                foreach (IWebElement file in filesList)
                {
                    IWebElement fileLink = file.FindElement(By.XPath(".//a[@class='brws-file-name-cell-filename']"));
                    String _fileName = file.FindElement(By.XPath(".//div[@class='brws-file-name-element']/span")).Text;
                    Assert.AreEqual(fileName, _fileName);
                }                              
            }
        }

        public void verifyFolderisSharedTo(string folderName, string sharedUser)
        {

            Boolean isShareMemeber = false;

            IWebElement _desiredFolderRow = findFolder(folderName);
            IWebElement _folderRowCheckBox = _desiredFolderRow.FindElement(By.XPath(".//span[@class='mc-checkbox-border']"));
            //_folderRowCheckBox.Click();
            //bu.mouseOverClick(FileDriver, _folderRowCheckBox,true);

            String strJavaScript = "var element = arguments[0];"
                    + "var mouseEventObj = document.createEvent('MouseEvents');"
                    + "mouseEventObj.initEvent( 'mouseover', true, true );"
                    + "element.dispatchEvent(mouseEventObj);";

            ////Then JavascriptExecutor class is used to execute the script to trigger the dispatched event.
            ((IJavaScriptExecutor)FileDriver).ExecuteScript(strJavaScript, _folderRowCheckBox);
            _folderRowCheckBox.Click();

            //IWebElement membersSection = _desiredFolderRow.FindElements(By.TagName("td"))[4];
            IWebElement membersTitle = _desiredFolderRow.FindElement(By.XPath(".//div[@class='shared-with-members clearfix']"));
            IWebElement audiencedescription = membersTitle.FindElement(By.XPath(".//div[@class='audience-description']"));

            Actions action = new Actions(FileDriver);
            action.MoveToElement(audiencedescription).Click(audiencedescription).Build().Perform();

            /*

            //members.Click();

            String _strJavaScript = "var element = arguments[0];"
                    + "var mouseEventObj = document.createEvent('MouseEvents');"
                    + "mouseEventObj.initEvent( 'mouseover', true, true );"
                    + "element.dispatchEvent(mouseEventObj);";

            ////Then JavascriptExecutor class is used to execute the script to trigger the dispatched event.
            ((IJavaScriptExecutor)FileDriver).ExecuteScript(_strJavaScript, membersTitle);
            membersTitle.Click();
            */
            Thread.Sleep(20000);

            IList<IWebElement> sharingMemeberList = FileDriver.FindElements(By.XPath(".//div[@class='sharing-member-list']/ul/div/li"));

            foreach (IWebElement sharingMemeber in sharingMemeberList)
            {
                String shareMemberName = sharingMemeber.FindElement(By.XPath(".//span[@class='scl-member-row-details-span']")).Text;
                if (shareMemberName.Equals(sharedUser))
                {
                    isShareMemeber = true;
                }
            }

            Assert.AreEqual(true, isShareMemeber);

            IWebElement cancelBtn = FileDriver.FindElement(By.XPath(".//button[@class='scl-sharing-modal-header__close mc-button-styleless']"));
            cancelBtn.Click();
        }
    }
}
