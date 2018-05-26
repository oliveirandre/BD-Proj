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
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ProjetoFinalBD
{
    /// <summary>
    /// Lógica interna para Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {

        private SqlConnection cn;

        private static int i = 0;
        private static int j = 1;
        private static int k = 2;
        private static string[] albums = new string[]
        {
            @"1.png",
            @"2.jpg",
            @"3.jpg",
            @"4.jpg",
            @"5.jpg"
        };

        public Window2()
        {
            InitializeComponent();

            Main.Source = new BitmapImage(new Uri(albums[j], UriKind.Relative));
            Right.Source = new BitmapImage(new Uri(albums[k], UriKind.Relative));
            Left.Source = new BitmapImage(new Uri(albums[i], UriKind.Relative));
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

        private void ButtonSlideRight_Click(object sender, RoutedEventArgs e)
        {
            i--; j--; k--;
            if (i < 0) i = albums.Length - 1; if (j < 0) j = albums.Length - 1; if (k < 0) k = albums.Length - 1;
            Main.Source = new BitmapImage(new Uri(albums[j], UriKind.Relative));
            Right.Source = new BitmapImage(new Uri(albums[k], UriKind.Relative));
            Left.Source = new BitmapImage(new Uri(albums[i], UriKind.Relative));
        }

        private void ButtonSlideLeft_Click(object sender, RoutedEventArgs e)
        {
            i--; j--; k--;
            if (i < 0) i = albums.Length - 1; if (j < 0) j = albums.Length - 1; if (k < 0) k = albums.Length - 1;
            Main.Source = new BitmapImage(new Uri(albums[j], UriKind.Relative));
            Left.Source = new BitmapImage(new Uri(albums[k], UriKind.Relative));
            Right.Source = new BitmapImage(new Uri(albums[i], UriKind.Relative));
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void GitHubItemSelected(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/oliveirandre/BD-Proj"));
        }

        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("data source= .\\SQLEXPRESS;integrated security=true;initial catalog=ProjetoFinalBD");
        }

        private bool verifySGBDConnection()
        {
            if (cn == null)
                cn = getSGBDConnection();

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }

        private void TestingAdd(object sender, RoutedEventArgs e)
        {
            /*if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT SampleKeeper.Account (real_name, nickname, password) " + "VALUES ('testname', 'testnickname', 'testpassword') ";

            cmd.Connection = cn;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update contact in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                cn.Close();
            }*/
        }
    }
}
