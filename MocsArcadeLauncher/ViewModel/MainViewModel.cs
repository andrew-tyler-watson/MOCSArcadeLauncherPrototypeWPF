using System;
using System.Collections.Generic;
using System.IO;
using DataLayer;
using GalaSoft.MvvmLight;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;


namespace MocsArcadeLauncher.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            
        }

        #region Properties

        private List<Game> _Games;
        public List<Game> Games
        {
            get
            {
                if (_Games == null)
                {
                    _Games = GetGamesFromDirectory();
                }
                return _Games;
            }
            set
            {
                _Games = value;
                RaisePropertyChanged();
            }
        }

        private Game _SelectedGame;

        public Game SelectedGame
        {
            get { return _SelectedGame; }
            set
            {
                _SelectedGame = value;
                RaisePropertyChanged();
            }
        }

        private string _PathToGameDirectory;
        public string PathToGameDirectory
        {
            get
            {
                return _PathToGameDirectory;
            }
            set
            {
                _PathToGameDirectory = value;
                RaisePropertyChanged();
            }
        }
        private string _PathToUpdaterDirectory;
        public string PathToUpdaterDirectory
        {
            get
            {
                return _PathToUpdaterDirectory;
            }
            set
            {
                _PathToUpdaterDirectory = value;
                RaisePropertyChanged();
            }
        }
        private string _PathToFocuserDirectory;
        public string PathToFocuserDirectory
        {
            get
            {
                return _PathToFocuserDirectory;
            }
            set
            {
                _PathToFocuserDirectory = value;
                RaisePropertyChanged();
            }
        }

        #endregion


        #region Functions

        public List<Game> GetGamesFromDirectory()
        {
            var output = new List<Game>();
            if (Properties.Settings.Default.GameRootDirectory == "")
            {
                return output;
            }

            var rootPath = Properties.Settings.Default.GameRootDirectory;
            var rootDir = new DirectoryInfo(rootPath);
            var gameDirs = rootDir.EnumerateDirectories();

            foreach (var dir in gameDirs)
            {
                output.Add(GetGameFromGameDirectory(dir));
            }

            //output[0].IsSelected = true;
            return output;
        }

        public Game GetGameFromGameDirectory(DirectoryInfo directoryInfo)
        {
            var gameName = directoryInfo.Name;
            var icoFiles = directoryInfo.GetFiles("*.ico");
            var iconPath = icoFiles[0].FullName;
            var psFiles = directoryInfo.GetFiles("*.ps1");
            var startScriptPath = psFiles[0].Name;


            return new Game()
            {
                Name = gameName,
                PathToIconFile = iconPath,
                PathToRunScript = startScriptPath,
                IsSelected = true
            };

        }

        public void PopulateUserSettingsProperties()
        {
            PathToGameDirectory = Properties.Settings.Default.GameRootDirectory;
            PathToFocuserDirectory = Properties.Settings.Default.FocusExecPath;
            PathToUpdaterDirectory = Properties.Settings.Default.UpdaterExecPath;
        }

        #endregion

        #region Commands

        private RelayCommand _SaveSettingsCommand;
        public RelayCommand SaveSettingsCommand
        {
            get
            {
                return _SaveSettingsCommand ?? (_SaveSettingsCommand = new RelayCommand(() => {
                    Properties.Settings.Default.UpdaterExecPath = PathToUpdaterDirectory;
                    Properties.Settings.Default.GameRootDirectory = PathToGameDirectory;
                    Properties.Settings.Default.FocusExecPath = PathToFocuserDirectory;
                    Properties.Settings.Default.Save();
                    Games = GetGamesFromDirectory();
                }));
            }
            set
            {
                _SaveSettingsCommand = value;
            }
        }

        #endregion


        public void HandleEscape()
        {
            Games[3].IsSelected = false;
        }
        public void RunGame()
        {
            //Run selected game based off of 
            //          Game.PathToRunScript
        }
    }
}