# MS Solution Launcher

Launch multiple instances of Visual Studio for Mac and/or Xamarin Studio easily!

You can also associate the application with all `.sln` files on your Mac to have them open in a new instance of Visual Studio for Mac or Xamarin Studio.

![MS Solution Launcher Screenshot](https://github.com/Redth/MSSolutionLauncher/raw/master/Art/screen-01.png)

## Download

Check out the [Releases](https://github.com/Redth/MSSolutionLauncher/releases) page to get the latest version!


## Associating all .sln files
If you associate all .sln files with MS Solution Launcher, any time you double click or otherwise open a .sln file, it will open inside its own instance of Visual Studio for Mac or Xamarin Studio!

![MS Solution Launcher Default for all .sln Solution Files](https://github.com/Redth/MSSolutionLauncher/raw/master/Art/finder-change.png)

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
 
 
## But the icon is old!

I'm terrible at making icons.  If you'd like to help make an icon that is beautiful and more relevant, please do!



## License

The MIT License (MIT)
Copyright (c) 2016 Jonathan Dick

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
