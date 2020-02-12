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
        }

        private void OnLoaded_SelectFirstListBoxItem(object sender, RoutedEventArgs e)
        {

            this.MainListbox.SelectedItem = MainListbox.Items[0];
            var listBoxItem = (ListBoxItem)MainListbox.ItemContainerGenerator.ContainerFromItem(MainListbox.SelectedItem);
            listBoxItem.Focus();
        }

        private void UIElement_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {


        }


        private void MainGrid_OnKeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Down)
            //{
            //    ((MainViewModel) this.DataContext).HandleUpArrow();
            //}
            //if (e.Key == Key.Up)
            //{ 
            //    ((MainViewModel)this.DataContext).HandleDownArrow();
            //}
            //if (e.Key == Key.Left)
            //{
            //    ((MainViewModel)this.DataContext).HandleLeftArrow();
            //}
            //if (e.Key == Key.Right)
            //{
            //    ((MainViewModel)this.DataContext).HandleRightArrow();
            //}
            if (e.Key == Key.Escape)
            {
                ((MainViewModel)this.DataContext).HandleEscape();
            }
            if (e.Key == Key.Enter)
            {
                var game = MainListbox.SelectedItem as Game;
                if (File.Exists(game.PathToRunScript))
                {
                    //File.GetAttributes("Start.ps1");
                    //string strCmdText = Path.Combine(Directory.GetCurrentDirectory(), "Start.ps1");
                    var process = new Process();
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.LoadUserProfile = true; 
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.FileName = "powershell";
                    process.StartInfo.WorkingDirectory = "C:/Users/andre/MocsArcade/BananaBomber/BananaBomber_StartScript";
                    process.StartInfo.Arguments = @"-ExecutionPolicy Unrestricted ./BananaBomber_StartScript.ps1"; 

                    process.Start();
                    process.WaitForExit();



                }
            }
        }
    }
}
