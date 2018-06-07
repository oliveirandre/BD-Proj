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
using System.Windows.Media.Effects;

namespace ProjetoFinalBD
{
    /// <summary>
    /// Lógica interna para Music2Playlist.xaml
    /// </summary>
    ///     public class Playlist

    public partial class Music2Playlist : Window
    {
        public int userid;
        public string user;
        public string song;
        private SqlConnection cn;
        List<Playlist> playlist_list = new List<Playlist>();

        public Music2Playlist(string user, int userid, string currentSong)
        {            
            InitializeComponent();
            this.user = user;
            this.userid = userid;
            this.song = currentSong;
            cn = getSGBDConnection();
            PopulatePlaylists();
        }

        public void PopulatePlaylists()
        {
            playlist_list.Clear();
            SqlDataReader SAIDA;
            SqlDataAdapter da2 = new SqlDataAdapter();
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            SqlCommand command = new SqlCommand("SELECT * FROM SampleKeeper.getPlaylists(@User_ID)", cn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@User_ID", userid);
            SAIDA = command.ExecuteReader();
            if (SAIDA.HasRows)
            {
                while (SAIDA.Read())
                {
                    playlist_list.Add(new Playlist(SAIDA.GetInt32(0), SAIDA.GetString(1)));
                }
            }
            SAIDA.Close();

            List<string> newlist = new List<string>();
            for(int i = 0; i < playlist_list.Count; i++)
            {
                newlist.Add(playlist_list[i].nome);
            }
            playlist.ItemsSource = newlist;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow window = new SearchWindow(user, userid);
            int id = 0;
            for(int i = 0; i < playlist_list.Count; i++)
            {
                if (playlist.SelectedItem.ToString() == playlist_list[i].nome)
                {
                    id = playlist_list[i].id;
                }
            }

            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SampleKeeper.add_music2playlist", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@playlist_ID", id);
            cmd.Parameters.AddWithValue("@isrc", song);

            cmd.Connection = cn;
            if (playlist.Text == "")
            {
                hidden.Content = "Please choose a playlist!";
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
