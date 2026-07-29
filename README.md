<p align="center">
<img width="512" height="512" alt="templateaddon" src="https://github.com/user-attachments/assets/0152579d-087b-4a10-b905-5453133feda2" />
</p>
<h1>Neurotrauma C# Addon Template</h1>
A premade template for Neurotrauma C# addons.
Follow the instructions below to use.

- Download the zip file and extract it to any folder of your choosing.
  
- Change the LocalMods folder directory in **Build.props** to fit your system. If your using windows you most likely can ignore this.
  
- Download the required refs from [here](https://github.com/evilfactory/LuaCsForBarotrauma/releases/download/latest/luacsforbarotrauma_refs.zip) and extract it to the "Refs" folder in the project. Note: you must create the folder.

- Download Visual Studio (Not Visual Code Studio!!!) with the .NET addon, then open the Addon.sln file using it to have the entire project visible and easily navigatable.

- If you want to test, you should go to the top side of the screen and under 'Build' hit 'Rebuild Solution'; this will re-generate the entire LocalMod. You can then Launch Barotrauma via Visual Studio or the normal way.

- Everything within the **_Assets_** folder gets copied into LocalMods alongside the C# code, already compiled.
