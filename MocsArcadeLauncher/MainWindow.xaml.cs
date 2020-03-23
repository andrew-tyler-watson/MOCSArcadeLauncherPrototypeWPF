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
            FocusManager.SetFocusedElement(this, this);
            Loaded += OnLoaded_SelectFirstListBoxItem;
            StartHelperProcesses();
            
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

        private void UIElement_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {


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


        private void MainGrid_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                ((MainViewModel)this.DataContext).HandleEscape();
            }
            if (e.Key == Key.Enter)
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
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;
        }

        private void StartHelperProcesses()
        {
            Updater.Start();
            //Focuser.Start();
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

            process.StartInfo.Arguments = @"-ExecutionPolicy Unrestricted .\Main.exe " + Properties.Settings.Default.GameRootDirectory;

            return process;
        }
        private Process InitializeFocuser()
        {
            var process = new Process();
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.LoadUserProfile = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.FileName = "powershell";
            process.StartInfo.WorkingDirectory = Properties.Settings.Default.UpdaterExecPath;

            process.StartInfo.Arguments = @"-ExecutionPolicy Unrestricted .\Main.exe ";
            return process;
        }
        private void StopHelperProcesses()
        {
            Updater.Kill();
            //Focuser.Kill();
        }
    }
}
