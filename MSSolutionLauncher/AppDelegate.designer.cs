// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SolutionLauncher
{
	partial class AppDelegate
	{
		[Outlet]
		AppKit.NSMenuItem fileOpen { get; set; }

		[Outlet]
		AppKit.NSMenuItem fileOpenRecent { get; set; }

		[Outlet]
		AppKit.NSMenuItem helpHelp { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (fileOpen != null) {
				fileOpen.Dispose ();
				fileOpen = null;
			}

			if (fileOpenRecent != null) {
				fileOpenRecent.Dispose ();
				fileOpenRecent = null;
			}

			if (helpHelp != null) {
				helpHelp.Dispose ();
				helpHelp = null;
			}
		}
	}
}
