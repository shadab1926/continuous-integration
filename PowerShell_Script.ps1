#obtaining the name of recently added sub folder to the "Builds" folder in remote machine
$dir = "\\cest01\builds\\Builds "
$latest = Get-ChildItem -Path $dir | Sort-Object LastAccessTime -Descending | Select-Object -First 1
$RecentBuildNo = $latest.name
$RecentBuildNo

$source = "\\cest01\builds\\Builds\$RecentBuildNo"
$dest = "C:\temp"



#current system date storing in the variable
$TimeStart = Get-Date
#defining the time when u want the installation to start
[datetime]$MyDate = "21:00:00"
Do 
{ 
    $TimeNow = Get-Date
    if ($TimeNow -ge $MyDate) 
    {
    #command to copy the setup files
    Copy-Item $source -Destination $dest -Recurse -Force 
    
    #dynamically creating the bat file
    $Arguments = '/v" /qn"'
    $Arg = '{0}' -f $Arguments
    echo C:\temp\$RecentBuildNo\Web.exe` /s` $Arg | Out-File -Encoding default C:\temp\$RecentBuildNo\install.bat
    
    #Invoke the bat file
    Start-Process -FilePath "C:\temp\$RecentBuildNo\install.bat" -Wait

    #start Automation if u want to 
    Start-Process -FilePath "C:\Users\myPc\Desktop\BatchScriptTest\BatchScriptNew.bat"
    } 
    else 
    {
         Write-Host "Not done yet, it's only $TimeNow"
    }
    #set a sleep time for periodic check to satisfy the condition
 Start-Sleep -Seconds 10
}
Until ($TimeNow -ge $MyDate)
