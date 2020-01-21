using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataLayer;

namespace MocsArcadeLauncher
{
    /// <summary>
    /// Interaction logic for GameTileUserControl.xaml
    /// </summary>
    public partial class GameTileUserControl : UserControl
    {
        public GameTileUserControl()
        {
            InitializeComponent();
            //Loaded += OnLoaded;
        }

        //private void OnLoaded(object sender, RoutedEventArgs e)
        //{
        //    var Gamey = ((Game)DataContext);
        //    Binding bind = new Binding("IsSelected") {Source = Gamey, Mode = BindingMode.TwoWay};
        //    this.SetBinding(IsSelectedProperty, bind);
        //}

        //public static readonly DependencyProperty IsSelectedProperty =
        //    DependencyProperty.Register(
        //        "IsSelected", typeof(Boolean),
        //        typeof(GameTileUserControl), new PropertyMetadata(OnSelectedChangedCallBack)
        //    );
        //public bool IsSelected
        //{
        //    get
        //    {
        //        return (bool)GetValue(IsSelectedProperty);
        //    }
        //    set
        //    {
        //        SetValue(IsSelectedProperty, value);
                
        //    }
        //}

        //private static void OnSelectedChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        //{
        //    var senderTile = sender as GameTileUserControl;
        //    if ((bool)e.NewValue)
        //    {
        //        senderTile.SelectionBorder.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        senderTile.SelectionBorder.Visibility = Visibility.Collapsed;
        //    }
        //}

        private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
        {
           // if()
        }
    }
}
