using System;
using System.Collections.Generic;
using System.IO;
using DataLayer;
using GalaSoft.MvvmLight;

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

        public void GiveFocusManagerListOfGameTiles(List<GameTileUserControl> Tiles)
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

        #endregion


        


        public List<Game> GetGamesFromDirectory()
        {
            var output = new List<Game>();

            var rootPath = "C:/Users/andre/MocsArcade";
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
            var dirFiles = directoryInfo.GetFiles("*.ico");
            var iconPath = dirFiles[0].FullName;
            var startScriptPath = directoryInfo.GetDirectories("*StartScript*")[0].GetFiles(gameName + "_StartScript*")[0].FullName;


            return new Game()
            {
                Name = gameName,
                PathToIconFile = iconPath,
                PathToRunScript = startScriptPath,
                IsSelected = true
            };

        }

        public void HandleUpArrow()
        {
            SelectedGame.IsSelected = false;
        }
        public void HandleDownArrow()
        {
            SelectedGame.IsSelected = false;
        }
        public void HandleLeftArrow()
        {
            SelectedGame.IsSelected = false;
        }
        public void HandleRightArrow()
        {
            SelectedGame.IsSelected = false;
        }

        public void HandleEscape()
        {
            Games[3].IsSelected = false;
        }


        public void SelectGame(int selection)
        {
            SelectedGame = Games[selection];
        }

        public void SelectGame(Game selection)
        {
            SelectedGame = selection;
        }

        public void RunGame()
        {
            //Run selected game based off of 
            //          Game.PathToRunScript
        }
    }
}