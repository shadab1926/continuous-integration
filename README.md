# continuous-integration

# What it does.?
continuous deployment of .exe or .msi packages using powershell and .net forms.

# If you wanna see whether there is any new build(.exe) arrived has arrived in the remote machine, mail the team members about the build, copy the build to the local machine, install the same in the local machine and run automation.... 


# All this can be done silently in background

# below are the steps that can explain in more detail. 
1) The .net form application identifies it and its build No, prompt the local user with the message box "new build has arrived", mail the team members that the new build has been arrived

2) Involks the Powershell script which is copied to the local location, This  script copies the .exe from remote machine to local machine based on the location you provided by the user.

3) the script creates a dynamic batch file which instals the .exe file in local machine Silently.

4) After installation run the automation testing if needed.

5) User can even set the time between when the build has to be copied and installed irrespective to when the build has arrived

# Extraxt the exe of .net form application install in the machine run the application.. <- thats all u need to worry about


