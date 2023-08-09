# Backlighter
A tiny program that keeps the backlighting on the Logitech MX Keys permanently on. The program sends USB control messages to disable, and then reenable the backlight on your MX Keys keyboard every 180 seconds. It's meant to be used whilst charging. I haven't found a way to turn the light on without (very) briefly turning it off, so occasionally your keyboard light may flicker briefly every 3 minutes. If someone finds a way to send a USB (HID) command that doesn't require disabling the backlight, let me know :)

# Compile instructions
1) Install Visual Studio Community 2022 with the '.NET desktop development' Workload
2) Clone or download this repository, and open the .sln file with Visual Studio Community 2022.
3) In Visual Studio:
  - Click 'Tools' -> 'Nuget Package Manager' -> 'Manage NuGet Packages For Solution...'
  - Click 'Browse' tab -> Search for 'hid.net'
    - Click to highlight 'Hid.Net - by Christian Findlay'
      - Tick 'Backlighter.csproj' and click 'Install'
4) Click 'Debug' dropdown in the top bar, and set to 'Release'
5) Click 'Build' -> 'Rebuild Solution'
6) Your project should build and the file '.\bin\Backlighter.exe' will be compiled
7) Go into the Backlighter directory, and find the \bin\Backlighter.exe file
8) Right-click the file -> 'Create shortcut'
9) Right-click 'Start' -> 'Run', and type 'shell:startup' to open the startup folder
10) Copy the shortcut you just created into the Startup folder
