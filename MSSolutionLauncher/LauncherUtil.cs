using System;
using System.Collections.Generic;
using System.Diagnostics;
using AppKit;
using Foundation;

namespace SolutionLauncher
{
    public static class LauncherUtil
    {
        static readonly Dictionary<SlnOpenWith, List<string>> APP_IDS = new Dictionary<SlnOpenWith, List<string>> {
            {
                SlnOpenWith.XamarinStudio, 
                new List<string> {
                    "com.xamarin.xamarinstudio",
                    "com.xamarin.monodevelop"
                }
            },
            {
                SlnOpenWith.VisualStudio,
                new List<string> {
                    "com.microsoft.visual-studio"
                }
            },
        };

        const string MAC_OPEN = "/usr/bin/open";
        const string FIND_APP = "/usr/bin/mdfind";
        const string FIND_APP_ARGS = "kMDItemCFBundleIdentifier=\"{0}\"";


        public static void Launch()
        {
            Launch(null);
        }

        static string visualStudioBundleId = null;
        static string VisualStudioBundleId
        {
            get
            {
                if (visualStudioBundleId == null)
                    visualStudioBundleId = FirstBundleIdThatExists(APP_IDS[SlnOpenWith.VisualStudio].ToArray());
                return visualStudioBundleId;
            }
        }
        static string xamarinStudioBundleId = null;
        static string XamarinStudioBundleId
        {
            get
            {
                if (xamarinStudioBundleId == null)
                    xamarinStudioBundleId = FirstBundleIdThatExists(APP_IDS[SlnOpenWith.XamarinStudio].ToArray());
                return xamarinStudioBundleId;
            }
        }

        public static void Launch (string slnFile)
        {
            string appId = string.Empty;

            var openWith = Preferences.Instance.OpenWith;

            // If we are supposed to ask AND 
            if (openWith == SlnOpenWith.Ask)
            {
                // Even if we're supposed to ask, make sure more than one choice exists to use, otherwise
                // it's pointless to ask which to pick
                if (!string.IsNullOrEmpty(VisualStudioBundleId) && !string.IsNullOrEmpty(XamarinStudioBundleId))
                {
                    var askOpenWith = ChooseIde(slnFile);
                    if (askOpenWith.HasValue)
                        openWith = askOpenWith.Value;
                    else
                        return;
                }
            }

            // Decide which bundle id to use
            if (openWith == SlnOpenWith.VisualStudio)
                appId = VisualStudioBundleId;
            else if (openWith == SlnOpenWith.XamarinStudio)
                appId = XamarinStudioBundleId;

            // If no bundle ids, just don't launch anything
            if (string.IsNullOrEmpty(appId))
                return;

            var args = new List<string> {
                "-n",        // New Instance
                "-b", appId, // Open by bundle id
            };

            // See if we are asked to open a file
            if (!string.IsNullOrEmpty(slnFile))
            {
                args.Add("--args");
                args.Add("\"" + slnFile + "\""); // .sln file requested to open, quoted
            }

            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = MAC_OPEN,
                    Arguments = string.Join(" ", args),
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    ErrorDialog = false,
                    //RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                }
            };

            var path = "";
            if (p.StartInfo.EnvironmentVariables.ContainsKey("PATH"))
                path = p.StartInfo.EnvironmentVariables["PATH"] ?? "";
            path = path.TrimEnd(':') + ":/usr/local/bin";
            path = path.TrimStart(':');

            if (!p.StartInfo.EnvironmentVariables.ContainsKey("PATH"))
                p.StartInfo.EnvironmentVariables.Add("PATH", path);
            else
                p.StartInfo.EnvironmentVariables["PATH"] = path;

            p.Start();
            p.WaitForExit();

            var stdout = p.StandardOutput.ReadToEnd();
            var stderr = p.StandardError.ReadToEnd();

            if (!string.IsNullOrEmpty(stdout))
                Console.WriteLine(stdout);

            if (!string.IsNullOrEmpty(stderr))
                Console.WriteLine(stderr);
        }


        public static string FirstBundleIdThatExists(params string[] possibleBundleIds)
        {
            foreach (var bundleId in possibleBundleIds)
            {
                var o = RunTask(FIND_APP, string.Format(FIND_APP_ARGS, bundleId));

                if (!string.IsNullOrWhiteSpace(o))
                    return bundleId;
            }
            return null;
        }

        public static string RunTask(string task, params string[] args)
        {
            var r = string.Empty;

            try
            {
                var pipeOut = new NSPipe();

                var t = new NSTask();
                t.LaunchPath = task;
                if (args != null)
                    t.Arguments = args;

                var path = "/usr/local/bin";
                var env = new NSMutableDictionary();
                env.SetValueForKey(new NSString(path), new NSString("PATH"));

                t.Environment = env;

                t.StandardOutput = pipeOut;
                t.StandardError = pipeOut;

                t.Launch();
                t.WaitUntilExit();
                //t.Release ();

                r = pipeOut.ReadHandle.ReadDataToEndOfFile().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(task + " failed: " + ex);
            }
            return r;
        }

        public static SlnOpenWith? ChooseIde (string openSlnFile)
        {
            var alert = new NSAlert();
            alert.InformativeText = "MS Solution Launcher allows you to easily open multiple instances of Xamarin Studio or Visual Studio for Mac";

            alert.AddButton("Visual Studio for Mac");
            alert.AddButton("Xamarin Studio");
            alert.AddButton("Cancel");

            if (!string.IsNullOrEmpty(openSlnFile))
            {
                alert.MessageText = "Which application would you like to open this solution with?";
                alert.InformativeText = openSlnFile;
            }
            else
            {
                alert.MessageText = "Which application would you like to open?";
                alert.InformativeText = "A new instance of the application you choose will be opened.";
            }

            var which = alert.RunModal();

            var buttonIndex = which - (int)NSAlertButtonReturn.First;

            switch (buttonIndex)
            {
                case 0:
                    return SlnOpenWith.VisualStudio;
                case 1:
                    return SlnOpenWith.XamarinStudio;
            }

            return null;
        }
    }
}
