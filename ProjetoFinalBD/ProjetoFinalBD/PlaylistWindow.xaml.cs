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
    /// Lógica interna para PlaylistWindow.xaml
    /// </summary>
    /// 

    public class Playlist
    {
        public string nome;
        public int id;

        public Playlist(int id, string nome)
        {
            this.nome = nome;
            this.id = id;
        }
    }

    public class Table_Entry
    {
        public string music_name;
        public string artist_name;
        public string album_name;
        public string duracao;

        public Table_Entry(string music, string artist, string album, string duracao)
        {
            this.music_name = music;
            this.artist_name = artist;
            this.album_name = album;
            this.duracao = duracao;
        }
    }

    public class MyItem
    {
        public string Nome { get; set; }
        public string Artista { get; set; }
        public string Album { get; set; }
        public string Duracao { get; set; }
        public string isrc { get; set; }
    }

    public class Semelhantes
    {
        public string isrc1;
        public string isrc2;

        public Semelhantes(string isrc1, string isrc2)
        {
            this.isrc1 = isrc1;
            this.isrc2 = isrc2;
        }
    }

    public partial class PlaylistWindow : Window
    {
        List<Playlist> playlist_list = new List<Playlist>();
        List<MyItem> table_list = new List<MyItem>();
        List<Semelhantes> sem_list = new List<Semelhantes>();
        private SqlConnection cn;
        public string user;
        public int userid;
        public string currentPlaylist;
        public MyItem currentRecomendation;
        public PlaylistWindow(string user, int userid)
        {
            InitializeComponent();
            EraseMusic();
            this.userid = userid;
            this.user = user;
            username.Text = user;
            cn = getSGBDConnection();
            PopulatePlaylists();
        }

        public void EraseMusic()
        {
            RecAlbumName.Content = null;
            RecArtistName.Content = null;
            RecDuration.Content = null;
            RecSongName.Content = null;
            plus.Visibility = Visibility.Collapsed;
            label1.Visibility = Visibility.Collapsed;
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
            for (int i = 0; i < playlist_list.Count; i++)
            {
                newlist.Add(playlist_list[i].nome);
            }
            PlaylistBox.ItemsSource = newlist;
        }

        private void Pbox_Changed(object sender, SelectionChangedEventArgs e)
        {

            // ... Get the ComboBox.
            var PlaylistBox = sender as ComboBox;
            // ... Set SelectedItem as Window Title.
            currentPlaylist = PlaylistBox.SelectedItem as string;
            EraseMusic();
            setList(currentPlaylist);            
        }

        public void setList(string value)
        {
            int id = 0;
            for (int i = 0; i < playlist_list.Count; i++)
            {
                if (playlist_list[i].nome == value)
                {
                    id = playlist_list[i].id;
                    break;
                }
            }

            table_list.Clear();
            listv.Items.Clear();
            SqlDataReader SAIDA;
            SqlDataAdapter da2 = new SqlDataAdapter();
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            SqlCommand command = new SqlCommand("SELECT * FROM SampleKeeper.getPlaylistMusics(@playlist_ID)", cn);
            command.Parameters.AddWithValue("@playlist_ID", id);
            command.CommandType = CommandType.Text;
            SAIDA = command.ExecuteReader();
            TimeSpan temp;
            if (SAIDA.HasRows)
            {
                while (SAIDA.Read())
                {
                    temp = SAIDA.GetTimeSpan(3);
                    table_list.Add(new MyItem { Nome = SAIDA.GetString(0), Artista = SAIDA.GetString(1), Album = SAIDA.GetString(2), Duracao = temp.ToString(), isrc = SAIDA.GetString(4) });
                }
            }
            SAIDA.Close();
            AuthorName.Content = "Created by " + user;
            for (int k = 0; k < table_list.Count(); k++)
            {
                this.listv.Items.Add(table_list[k]);
            }

            bool repeated = false;
            //Música recomendada
            if (table_list.Count != 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    repeated = false;
                    SqlDataReader reader;
                    Random rnd = new Random();
                    int r = rnd.Next(table_list.Count-1);
                    SqlCommand sql2 = new SqlCommand("SELECT * FROM SampleKeeper.getSimilar(@music1)", cn);
                    sql2.Parameters.AddWithValue("@music1", table_list[r].isrc);
                    sql2.CommandType = CommandType.Text;
                    reader = sql2.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            sem_list.Add(new Semelhantes(reader.GetString(0), reader.GetString(1)));                            
                        }
                    }
                    int r2 = rnd.Next(sem_list.Count-1);
                    string trmp;
                    Semelhantes tmp = sem_list[r2];
                    if (tmp.isrc1 == table_list[r].isrc)
                    {
                        trmp = tmp.isrc2;
                    }
                    else
                    {
                        trmp = tmp.isrc1;
                    }
                    reader.Close();
                    SqlDataReader reader2;
                    SqlCommand sql3 = new SqlCommand("SELECT * FROM SampleKeeper.getMusicWithISRC(@is)", cn);
                    sql3.Parameters.AddWithValue("@is", trmp);
                    sql3.CommandType = CommandType.Text;
                    reader2 = sql3.ExecuteReader();
                    TimeSpan temp2;
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            temp2 = reader2.GetTimeSpan(3);
                            currentRecomendation = new MyItem { Nome = reader2.GetString(0), Artista = reader2.GetString(1), Album = reader2.GetString(2), Duracao = temp2.ToString(), isrc = trmp };
                        }
                    }
                    reader2.Close();
                    for (int j = 0; j < table_list.Count; j++)
                    {
                        Console.WriteLine(currentRecomendation.isrc + "/"+ table_list[j].isrc);
                        if (currentRecomendation.isrc == table_list[j].isrc)
                        {
                            repeated = true;                            
                            break;
                        }

                    }
                    Console.WriteLine("--------");
                    if (currentRecomendation != null && !repeated)
                    {
                        Console.WriteLine("PASSOU");
                        setLabels(currentRecomendation);
                        break;
                    }
                }
            }

        }

        public void setLabels(MyItem currentRecomendation)
        {
            RecAlbumName.Content = currentRecomendation.Album;
            RecArtistName.Content = currentRecomendation.Artista;
            RecDuration.Content = currentRecomendation.Duracao;
            RecSongName.Content = currentRecomendation.Nome;
            plus.Visibility = Visibility.Visible;
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

        private void NewPlaylist(object sender, RoutedEventArgs e)
        {
            MainGrid.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;

            NewPlaylist win = new NewPlaylist(user, userid);
            win.ShowDialog();

            PopulatePlaylists();
            Splash.Visibility = Visibility.Collapsed;
            MainGrid.Effect = null;
        }

        private void Add2Playlist(object sender, RoutedEventArgs e)
        {
            // Adicionar à base de dados
            int play_id = 0;
            for(int i = 0; i < playlist_list.Count; i++)
            {
                if(playlist_list[i].nome == currentPlaylist)
                {
                    play_id = playlist_list[i].id;
                }
            }
            SqlCommand cmd = new SqlCommand("SampleKeeper.add_music2playlist", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@playlist_ID", play_id);
            cmd.Parameters.AddWithValue("@isrc", currentRecomendation.isrc);
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
            }
            // Dar reset ao "Recomended"
            EraseMusic();
            // Dar reset à lista
            setList(currentPlaylist);
            
        }

        private void RemovePlaylist(object sender, RoutedEventArgs e)
        {
            string value = PlaylistBox.SelectedItem as string;
            int id = 0;
            for (int i = 0; i < playlist_list.Count; i++)
            {
                if (playlist_list[i].nome == value)
                {
                    id = playlist_list[i].id;
                    break;
                }
            }

            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SampleKeeper.remove_playlist", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@playlist_ID", id);
            cmd.Connection = cn;
            if(PlaylistBox.SelectedItem == null)
            {
                hidden.Content = "Please select a playlist!";
            }
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
            PlaylistWindow pw = new PlaylistWindow(user, userid);
            pw.Show();
            this.Close();
        }

        private void RemoveSong(object sender, RoutedEventArgs e)
        {
            int id = 0;
            for (int i = 0; i < playlist_list.Count; i++)
            {
                if (playlist_list[i].nome == PlaylistBox.SelectedItem.ToString())
                {
                    id = playlist_list[i].id;
                }
            }
            MyItem index = (MyItem)listv.SelectedItem;

            if (index == null)
            {
                hidden.Content = "Please select a music to delete!";
            }
            else { 
                if (!verifySGBDConnection())
                    return;

                SqlCommand cmd = new SqlCommand("SampleKeeper.remove_musicplaylist", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@playlist_ID", id);
                cmd.Parameters.AddWithValue("@ISRC", index.isrc);
                cmd.Connection = cn;
                hidden.Content = null;
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
            }
            setList(currentPlaylist);
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
