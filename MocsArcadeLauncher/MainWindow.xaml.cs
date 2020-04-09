using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataLayer;
using MocsArcadeLauncher.ViewModel;

namespace MocsArcadeLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += OnLoaded_SelectFirstListBoxItem;
            StartHelperProcesses();
            //Q, E, Z, X, C, J, O, I, K, and L
        }

        private void OnLoaded_SelectFirstListBoxItem(object sender, RoutedEventArgs e)
        {
            FocusFirstListBoxItem();
            ((MainViewModel)DataContext).PopulateUserSettingsProperties();
        }

        private void FocusFirstListBoxItem()
        {
            if (MainListbox.Items.Count > 0)
            {
                this.MainListbox.SelectedItem = MainListbox.Items[0];
                var listBoxItem = (ListBoxItem)MainListbox.ItemContainerGenerator.ContainerFromItem(MainListbox.SelectedItem);
                listBoxItem.Focus();
            }
        }

        private Process _Updater;
        public Process Updater 
        {
            get
            {
                return _Updater??(_Updater = InitializeUpdater());
            }
            set
            {
                _Updater = value;
            }
        }
        private Process _Focuser;
        public Process Focuser
        {
            get
            {
                return _Focuser ?? (_Focuser = InitializeFocuser());
            }
            set
            {
                _Focuser = value;
            }
        }

        public bool ShouldStealFocus { get; set; }

        private void MainGrid_OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && Keyboard.IsKeyDown(Key.Escape))
            {
                Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
            else if (e.Key == Key.E && (Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl)))
            {
                ShouldStealFocus = !ShouldStealFocus;
                if (ShouldStealFocus)
                {
                    FocusNotifierTextBlock.Visibility = Visibility.Collapsed;
                }
                else
                {
                    FocusNotifierTextBlock.Visibility = Visibility.Visible;
                }
            }
            else if (e.Key == Key.Enter)
            {
                var game = MainListbox.SelectedItem as Game;

                //File.GetAttributes("Start.ps1");
                //string strCmdText = Path.Combine(Directory.GetCurrentDirectory(), "Start.ps1");
                var process = new Process();
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.LoadUserProfile = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.FileName = "powershell";
                process.StartInfo.WorkingDirectory = Properties.Settings.Default.GameRootDirectory + "/" + game.Name;

                process.StartInfo.Arguments = @"-ExecutionPolicy Unrestricted ./" + game.PathToRunScript;
                StopHelperProcesses();
                process.Start();
                process.WaitForExit();
                
                
            }
            if(e.Key == Key.S && (Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl)))
            {
                if(SettingsEditor.Visibility == Visibility.Visible)
                {
                    SettingsEditor.Visibility = Visibility.Collapsed;
                }
                else
                {
                    SettingsEditor.Visibility = Visibility.Visible;
                }
            }
            if(e.Key == Key.X && (Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl)))
            {
                Application.Current.Shutdown();
            }
            if(e.Key == Key.E)
            {
                UpdateGame(((Game)MainListbox.SelectedItem).Name);
            }

        }

        private void Window_Deactivated(object sender, EventArgs e)
        {

            MainWindow window = (MainWindow)sender;

            if (window.ShouldStealFocus)
            {
                window.Topmost = true;
                Focuser.Start();
            }
            else
            {
                window.Topmost = false;
            }

        }
        private void StartHelperProcesses()
        {
            Updater.Start();
        }

        private Process InitializeUpdater()
        {
            var process = new Process();
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.LoadUserProfile = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.FileName = "powershell";
            process.StartInfo.WorkingDirectory = Properties.Settings.Default.UpdaterExecPath;

            process.StartInfo.Arguments = "-ExecutionPolicy Unrestricted .\\Main.exe  \"" + Properties.Settings.Default.GameRootDirectory +"\"";

            return process;
        }

        private void UpdateGame(string gameName)
        {
            StopHelperProcesses();
            var oneTimeUpdater = InitializeUpdater();
            oneTimeUpdater.StartInfo.Arguments = "-ExecutionPolicy Unrestricted .\\Main.exe \"" + Properties.Settings.Default.GameRootDirectory + "\" \"" + gameName + "\"";
            oneTimeUpdater.Start();
            oneTimeUpdater.WaitForExit();
            Updater.Start();
        }

        private Process InitializeFocuser()
        {
            var process = new Process();
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.LoadUserProfile = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.FileName = "powershell";
            process.StartInfo.WorkingDirectory = Properties.Settings.Default.FocusExecPath;

            process.StartInfo.Arguments = @"-ExecutionPolicy Unrestricted .\focus.exe ";
            return process;
        }
        private void StopHelperProcesses()
        {
            try
            {
                Focuser.Kill();
                Updater.Kill();
            }
            catch(Exception err)
            {

            }

            
        }

    }
}
