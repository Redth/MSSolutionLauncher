# MS Solution Launcher

Launch multiple instances of Visual Studio for Mac and/or Xamarin Studio easily!

You can also associate the application with all `.sln` files on your Mac to have them open in a new instance of Visual Studio for Mac or Xamarin Studio.


## Associating all .sln files
If you associate all .sln files with MS Solution Launcher, any time you double click or otherwise open a .sln file, it will open inside its own instance of Visual Studio for Mac or Xamarin Studio!

 1. Find a .sln file in Finder
 2. Right click the .sln file and Get Info (or highlight the file and cmd + i
 3. Under the Open With section, click the drop-down list and click Choose
 4. Navigate to and select Xamarin Studio Launcher
 5. Click Change All
 
 
## Always open with...

By default, if you have both _Visual Studio for Mac_ and _Xamarin Studio_ installed, the launcher will ask you which one you'd like to open.  

If you'd rather have it **always** open in one by default, you can change this in the `Preferences.json` file located within the app's resources.

 1. Navigate to the `MS Solution Launcher.app` (in Finder, right click and _Show package contents_).
 2. Open the `Preferences.json` file located inside the `Contents/Resources` folder.
 3. Change the value for the key `OpenWith` to be either `Visual Studio` or `Xamarin Studio`.
 4. Save the file.
 
 
