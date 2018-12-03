# continuous-integration

# What it does.?
continuous deployment of .exe or .msi packages using powershell and .net forms.

# If you want to see weather there is any new arrived build(.exe) has arrived in the remote machine, mail the team members about the build, copy to the local machine, install the same inthe local machine and run automation.... 
# All this can be done silently in background

# below are the steps that can explain in more detail. 
1) The .net form application detects it and its build No, prompts the local user that "new build has arrived", mail the team members that the new build has been arrived

2) Involks the Powershell script copied to the local location where the script copies the .exe from remote machine to local machine based on the location u provided by the user.

3) Create a dinamic .bat file which instals the .exe file in local machine Silently.

4) After installation run the automation testing if needed.

5) User can can even set the time between when the build has to be copied and installed irrespective to wen the build has arrived

# Extraxt the exe of .net form application install in the machine run the application.. <- thats all u need to worry about


