using AppKit;
using Foundation;

namespace SolutionLauncher
{
    [Register("AppDelegate")]
    public partial class AppDelegate : NSApplicationDelegate
    {
        public AppDelegate()
        {
            Preferences.Load();
        }

        string slnFile = null;

        public override void DidFinishLaunching(NSNotification notification)
        {
            // Insert code here to initialize your application
            LauncherUtil.Launch(slnFile);

            // Kill ourselves
            NSApplication.SharedApplication.Terminate(this);
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }

        public override bool OpenFile(NSApplication sender, string filename)
        {
            // This gets called (if it does) before FinishedLaunching
            // So we should set this information here for which file to open
            slnFile = filename;
            return true;
        }
    }
}
