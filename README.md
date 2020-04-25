# GreenHeronEverywareAutomation
A Windows PowerShell Cmdlet that allows to control Green Heron's Everyware.

This PowerShell cmdlet allows the control of a Green Heron Everyware server from anywhere. It only requires the server to be reachable via the network. It can be used from any tools, programs, scripts, command line tools that run on Windows.

PowerShell registration (one time):

Download the dll, put it in a place you remember
Register it with your PowerShell environment by adding an entry to your user profile file at \WindowsPowerShell\Microsoft.PowerShell_profile.ps1: Import-Module \GreenHeronEverywareAutomation.dll
Save the user profile file, open a new PowerShell console and verify that you can call Invoke-GHSwitch
PowerShell usage:

NAME
Invoke-GHSwitch

SYNTAX
Invoke-GHSwitch [[-Server] ] [[-Port] ] [-Commands] <string[]> []

ALIASES
None

REMARKS
None

EXAMPLE (2 antenna switches are being switched with one command, any number is possible)

$c1 = 'SET_SWITCH||ANT1_1||A10'
$c2 = 'SET_SWITCH||80m 4 Square||NE (EU)'
$commands = @($c1, $c2)
Invoke-GHSwitch -Server localhost -Port 10000 -Commands $commands

Notes:

The || is a special separator between SET_SWITCH, name of switch and name of switch position. To find out what your names are, just copy and paste the commands from GH Everyware server window.
The software also supports the original special char separator of (char)31.
Convenient use from non-Windows tables:

I use this with an application called Touch Portal (https://www.touch-portal.com/). It allows the use of any Android or Apple tablet to switch the antennas without having to use the mouse (if I would use the GH Everyware client) or keyboard short cuts. The GH Everyware client or server do not even have to be in the foreground, they can be minimized or even run on a different computer. All I do is use my left hand and use the touch screen of my tablet. This allows for good automation that does not interrupt the operator (think contesting).

Touch Portal usage:

Register the PowerShell module as described above
Create a tiny PowerShell script, add your commands, as shown above. Save it as .ps1
Create a button, that executes a PowerShell script and choose the script you created above.
Create different small scripts for each button/switch you would like to with Touch Portal.
Create buttons for all of these.
