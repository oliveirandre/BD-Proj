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
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Navigation;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;


namespace ProjetoFinalBD
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class HomeWindow : Window
    {
        public string user;
        public int userid;

        public HomeWindow(string user, int userid)
        {            
            InitializeComponent();
            this.user = user;
            this.userid = userid;
            username.Text = user;
        }

        private void GitHubItemSelected(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/oliveirandre/BD-Proj"));
        }

        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow win3 = new LoginWindow();
            win3.Show();
            this.Close();
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void SearchItemSelected(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow(user, userid);
            win.Show();
            this.Close();
        }

        private void PlaylistSelected(object sender, RoutedEventArgs e)
        {
            PlaylistWindow win = new PlaylistWindow(user, userid);
            win.Show();
            this.Close();
        }

        private void HomeSelected(object sender, RoutedEventArgs e)
        {
            HomeWindow win = new HomeWindow(user, userid);
            win.Show();
            this.Close();
        }
    }
}
