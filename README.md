# ReCodeIt

ReCodeIt is a .net assembly remapper, it can take an assembly which has been de-obfuscated and remap classes to more usable names based on patterns. It ***requires*** a de-obfuscated dll to work.

# Building
- fork or clone the project to a local directory
- Make sure you have  [.net 8 sdk](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed or install it through the visual studio installer.
- Open the solution in visual studio and build
- Run the project from visual studio or the build folder.

# Using
- Start the application, go to the settings page and set the directories and options to your liking. I will provide further documentation down the line when the project is in a less chaotic state. For now using it works, there may be a GUI bug here or there.

# Planned Features
- Cross Mapping - The ability to code in a de-obfuscated state and then convert your code back to a state the original assembly understands once your done. This provide the ability to rename things to whatever you want and use them.
- De-Obfuscation - Taking a obfuscated dll and de-obfuscating it
