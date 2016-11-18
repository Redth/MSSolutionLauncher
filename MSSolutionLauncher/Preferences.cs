using System;
using System.Json;
using System.IO;
using Foundation;

namespace SolutionLauncher
{
    public class Preferences
    {
        public static Preferences Instance { get; private set; } = new Preferences();

        public Preferences()
        {
            OpenWith = SlnOpenWith.Ask;
        }

        public SlnOpenWith OpenWith
        {
            get;
            set;
        }

        public static void Load()
        {
            var file = Path.Combine(NSBundle.MainBundle.ResourcePath, "Preferences.json");

            if (!File.Exists(file))
                return;
            
            var json = System.Json.JsonObject.Load(File.OpenRead(file));

            var openWithStr = json["OpenWith"].ToString().Replace("\"", "").Replace(" ", "").ToLowerInvariant();

            Preferences.Instance.OpenWith = SlnOpenWith.Ask;

            if (openWithStr.Equals("xamarinstudio", StringComparison.InvariantCulture)
                || openWithStr.Equals("monodevelop", StringComparison.InvariantCulture))
            {
                Preferences.Instance.OpenWith = SlnOpenWith.XamarinStudio;
            }
            else if (openWithStr.Equals("visualstudio", StringComparison.InvariantCulture)
                     || openWithStr.Equals("visualstudioformac", StringComparison.InvariantCulture)
                     || openWithStr.Equals("visualstudiomac", StringComparison.InvariantCulture))
            {
                Preferences.Instance.OpenWith = SlnOpenWith.VisualStudio;
            }
        }
    }
}
