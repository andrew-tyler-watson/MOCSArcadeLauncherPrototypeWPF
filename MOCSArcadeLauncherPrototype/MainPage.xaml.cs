using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DataLayer;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MOCSArcadeLauncherPrototype
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Games = GetGamesFromDirectory();
        }

        public List<Game> Games { get; set; }

        public List<Game> GetGamesFromDirectory()
        {
            var output = new List<Game>();

            var rootPath = "C:/Users/andre/Downloads";
            var rootDir = new DirectoryInfo(rootPath);
            var gameDirs = rootDir.EnumerateDirectories();

            foreach (var dir in gameDirs)
            {
                output.Add(GetGameFromGameDirectory(dir));
            }

            return output;
        }

        public Game GetGameFromGameDirectory(DirectoryInfo directoryInfo)
        {
            var iconPath = directoryInfo.GetFiles(".ico")[0].FullName;
            var startScriptPath = directoryInfo.GetDirectories("StartScript")[0].FullName;
            var gameName = directoryInfo.Name;

            return new Game()
            {
                Name = gameName, PathToIconFile = iconPath, PathToRunScript = startScriptPath
            };

        }
    }
}
