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
    /// Lógica interna para NewSong.xaml
    /// </summary>
    public partial class NewSong : Window
    {
        private SqlConnection cn;
        List<Album> albumlist;
        List<Genre> genrelist;
        public string user;
        public int userid;
        public NewSong(string user, int userid)
        {
            
            InitializeComponent();
            this.user = user;
            this.userid = userid;
            SearchWindow sw = new SearchWindow(user, userid);

            List<string> tmp = new List<string>();
            albumlist = sw.album_list;
            for (int i = 0; i < albumlist.Count; i++)
            {
                tmp.Add(albumlist[i].nome);
            }
            albumbox.ItemsSource = tmp;

            List<string> tmp2 = new List<string>();
            genrelist = sw.genre_list;
            for (int i = 0; i < genrelist.Count; i++)
            {
                tmp2.Add(genrelist[i].nome);
            }
            genre.ItemsSource = tmp2;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow sw = new SearchWindow(user, userid);
            albumlist = sw.album_list;
            genrelist = sw.genre_list;
            string var = "";
            int var2 = 0;
            for (int i = 0; i < albumlist.Count; i++)
            {
                if (albumlist[i].nome == albumbox.SelectedItem.ToString())
                {
                    var = albumlist[i].catalog_number;
                    break;
                }
            }
            for (int i = 0; i < genrelist.Count; i++)
            {
                if (genrelist[i].nome == genre.SelectedItem.ToString())
                {
                    var2 = genrelist[i].id;
                    Console.Write(var2);
                    break;
                }
            }
            
            DateTime timestamp = DateTime.ParseExact(duration.Text, "mm:ss", System.Globalization.CultureInfo.CurrentCulture);

            if (!verifySGBDConnection())
                return;
            
            SqlCommand cmd = new SqlCommand("SampleKeeper.add_music", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nome", song.Text);
            cmd.Parameters.AddWithValue("@ISRC", ISRC.Text);
            cmd.Parameters.AddWithValue("@duracao", timestamp);
            cmd.Parameters.AddWithValue("@album", var);
            cmd.Parameters.AddWithValue("@genero", var2);
            // para alterar?
            cmd.Parameters.AddWithValue("@path_to", song.Text + ".mp3");

            cmd.Connection = cn;
            if (song.Text == "" || albumbox.SelectedItem == null || genre.SelectedItem == null || duration.Text == "" || ISRC.Text == "")
            {
                hidden.Content = "You must fill in all the fields!";
            }
            else if(ISRC.GetLineLength(0) < 15)
            {
                hidden.Content = "ISRC must have 15 characters!";
            }
            else
            {
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
                }
                this.Close();
            }
        }

        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("data source= .\\SQLEXPRESS;integrated security=true;initial catalog=ProjBD");
        }

        private bool verifySGBDConnection()
        {
            if (cn == null)
                cn = getSGBDConnection();

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }
    }
}
