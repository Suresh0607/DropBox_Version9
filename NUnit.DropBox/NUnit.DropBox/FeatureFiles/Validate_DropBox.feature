Feature: Validate DropBox Folder share functionality,
		 Create folder and Upload files in DropBox
		 And then share the Folder to any user.


Scenario Outline: Verify user can upload files to a folder and can Share
	Given user <userName> with <passWord> logs into the <DropBox_Application>
	Then I should see the 'Home' as PageHeader
	Then  user NaviateTo 'Files'
	Then I should see the 'Dropbox' as PageTitle
	Then  user creates NewFolder <FolderName>
	Then  user should see the newly create Folder <FolderName>
	Then  user upload files <UploadFileNames> to <FolderName>
	Then  verify newly Upload files <UploadFileNames> exists under the folder <FolderName>
	Then  user can share <FolderName> to <ShareFolderTo>
	Then  verify folder <FolderName> is shared to <ShareFolderTo>
	Then  user delete the <FolderName>
	Then  verify folder <FolderName> is deleted
	And   user Logs out of the application

Examples:
    | userName                      | passWord    | DropBox_Application           | FolderName | UploadFileNames | ShareFolderTo		 |
    | autoTesting.user.01@gmail.com | Sydney@0607 | https://www.dropbox.com/login | TestFolder | TestFile_1.txt  | sureshnammi@gmail.com |