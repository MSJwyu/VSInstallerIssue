# VS Installer Issue
Steps to reproduce:

1- Ensure that VS Installer Projects is installed (https://marketplace.visualstudio.com/items?itemName=VisualStudioProductTeam.MicrosoftVisualStudio2017InstallerProjects)  
2- Open the solution  
3- Right-click on the TestSetup project and click "Install"  
4- Run through the installer steps  
5- Ensure that the service installed correctly by finding "TestService" in Services list  
6- Start the service (if it isn't already)  
7- Go back into the solution  
8- Right-click on the TestService project and click Properties  
9- Click Assembly Information  
10- Increment both Assembly version and File version  
11- Click ok  
12- Rebuild project 
13- Click on the TestSetup project  
14- Scroll down in the properties to "Version"  
15- Increment this number  
16- Generate a new ProductCode  
17- Rebuild the setup project  
18- Right-click the setup project and click Install  
19- Follow the installer prompts  
20- You will notice that the project will not stop the service and update the files, instead, it will want you to continue, which requires a restart. This is the main problem we are having, as this needs to be part of our automated MDM process.   
