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

namespace ProjetoFinalBD
{
    /// <summary>
    /// Lógica interna para Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {
        private static int i = 0;
        private static int j = 1;
        private static int k = 2;
        private static string[] albums = new string[]   
        {
            @"C:\Users\Andre\Desktop\BD-Proj\ProjetoFinalBD\ProjetoFinalBD\1.png",
            @"C:\Users\Andre\Desktop\BD-Proj\ProjetoFinalBD\ProjetoFinalBD\2.jpg",
            @"C:\Users\Andre\Desktop\BD-Proj\ProjetoFinalBD\ProjetoFinalBD\3.jpg",
            @"C:\Users\Andre\Desktop\BD-Proj\ProjetoFinalBD\ProjetoFinalBD\4.jpg",
            @"C:\Users\Andre\Desktop\BD-Proj\ProjetoFinalBD\ProjetoFinalBD\5.jpg"
        };

        public Window1()
        {
            InitializeComponent();
            Principal.Source = new BitmapImage(new Uri(albums[j]));
            Sec1.Source = new BitmapImage(new Uri(albums[k]));
            Sec2.Source = new BitmapImage(new Uri(albums[i]));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            i++; j++; k++;
            if (k == albums.Length) k = 0;
            if (j == albums.Length) j = 0; if (i == albums.Length) i = 0;
            Principal.Source = new BitmapImage(new Uri(albums[j]));
            Sec1.Source = new BitmapImage(new Uri(albums[k]));
            Sec2.Source = new BitmapImage(new Uri(albums[i]));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            i--; j--; k--;
            if (i < 0) i = albums.Length-1; if (j < 0) j = albums.Length - 1; if (k < 0) k = albums.Length - 1;
            Principal.Source = new BitmapImage(new Uri(albums[j]));
            Sec1.Source = new BitmapImage(new Uri(albums[k]));
            Sec2.Source = new BitmapImage(new Uri(albums[i]));
        }
    }
}
