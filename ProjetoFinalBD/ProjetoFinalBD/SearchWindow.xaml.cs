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
    //A fazeres:
    //1) Criar class album
    //2) Mal programa arranca, popular lista de albuns
    //3) Adicionar objectos na base de dados com respectivas capas
    //4) ??????
    //5) Profit

    /// <summary>
    /// Lógica interna para Window2.xaml
    /// </summary>
    public class Album
    {
        public string catalog_number;
        public string nome;
        public int ano;
        public string editora;
        public string artista;
        public string image;

        public Album(string Catalog_Num, string Album_Name, int Album_Year, string Artist_Name, string Publisher_Name, string Image_File)
        {
            this.catalog_number = Catalog_Num;
            this.nome = Album_Name;
            this.ano = Album_Year;
            this.editora = Publisher_Name;
            this.artista = Artist_Name;
            this.image = Image_File;
        }

        public override string ToString()
        {
            return this.nome;
        }
    }
   
    public class Current
    {
        public string currentSong;
        public string currentAlbum;
        public string currentUser;
    }

    public class Musica
    {
        public string nome;
        public string genero;
        public TimeSpan duracao;
        public string isrc;

        public Musica(string Music_Name, string Genre, TimeSpan duration, string isrc)
        {
            this.nome = Music_Name;
            this.genero = Genre;
            this.duracao = duration;
            this.isrc = isrc;
        }
    }

    public class Sample
    {
        public string nome;
        public string criador;
        public TimeSpan duracao;
        public string tipo;
        public int id;

        public Sample(string Sample_Name, string Creator, TimeSpan duration, string Type, int id)
        {
            this.nome = Sample_Name;
            this.criador = Creator;
            this.duracao = duration;
            this.tipo = Type;
            this.id = id;
        }
    }

    public class Genre
    {
        public string nome;
        public int id;

        public Genre(int id, string nome)
        {
            this.id = id;
            this.nome = nome;
        }
    }


    public partial class SearchWindow : Window
    {
        public string user;
        public int userid;
        private SqlConnection cn;

        private static int j = 0;
        private static int i = 1;
        private static int k = 2;
        private static string album_base_dir = @"/Covers/";
        public List<Album> album_list = new List<Album>(); //Preencher isto automaticamente quando a window inicia
        public List<Musica> music_list = new List<Musica>();
        List<Sample> sample_list = new List<Sample>();
        public List<Genre> genre_list = new List<Genre>();
        public Current c = new Current();

        public SearchWindow(string user, int userid)
        {
            InitializeComponent();
            this.userid = userid;
            this.user = user;
            username.Text = user;
            cn = getSGBDConnection();
            PopulateAlbums();
            fillGenreList();
            Main.Source = new BitmapImage(new Uri(album_base_dir + album_list[i].image, UriKind.Relative));
            Right.Source = new BitmapImage(new Uri(album_base_dir + album_list[j].image, UriKind.Relative));
            Left.Source = new BitmapImage(new Uri(album_base_dir + album_list[k].image, UriKind.Relative));
            SetInfo(album_list[i]);
            button.Visibility = Visibility.Collapsed;            
        }

        public void PopulateAlbums()
        {
            j = 0;
            i = 1;
            k = 2;
            album_list.Clear();
            SqlDataReader SAIDA;
            SqlDataAdapter da2 = new SqlDataAdapter();
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            SqlCommand command = new SqlCommand("SELECT * FROM SampleKeeper.populate_albums()", cn);
            command.CommandType = CommandType.Text;
            SAIDA = command.ExecuteReader();
            if (SAIDA.HasRows)
            {
                while (SAIDA.Read())
                {
                    album_list.Add(new Album(SAIDA.GetString(0) , SAIDA.GetString(1), SAIDA.GetInt32(2), SAIDA.GetString(3), SAIDA.GetString(4), SAIDA.GetString(5)));
                }
            }
            SAIDA.Close();
        }

        public void SetInfo(Album alb)
        {
            c.currentAlbum = alb.nome;
            //Album Content
            AlbumName.Content = alb.nome;
            Artist.Content = alb.artista;
            YearOfRelease.Content = alb.ano;
            Editora.Content = alb.editora;
            //Music Content
            set_SongsBox(alb.nome);
        }

        public void set_SongsBox(string Album_Name)
        {
            music_list.Clear();
            List<string> songs = new List<string>();
            SqlDataReader result;
            SqlDataAdapter da2 = new SqlDataAdapter();
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            SqlCommand command = new SqlCommand("SELECT * FROM SampleKeeper.getalbummusics(@a)", cn);
            command.Parameters.AddWithValue("@a", Album_Name);
            result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    music_list.Add(new Musica(result.GetString(0), result.GetString(1), result.GetTimeSpan(2), result.GetString(3)));
                }
            }
            for(int i = 0; i < music_list.Count; i++)
            {
                songs.Add(music_list[i].nome);
            }
            result.Close();
            SongsBox.ItemsSource = songs;
        }

        private void SongsBox_Change(object sender, SelectionChangedEventArgs e)
        {
            
            // ... Get the ComboBox.
            var SongsBox = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            string value = SongsBox.SelectedItem as string;
            if(value != null)
            {
                button.Visibility = Visibility.Visible;
            }
            for (int i = 0; i < music_list.Count; i++)
            {
                if(music_list[i].nome == value)
                {
                    c.currentSong = music_list[i].isrc;
                    SongGenre.Content = music_list[i].genero;
                    SongDuration.Content = music_list[i].duracao.ToString();
                    break;
                }
            }
            //Set the SamplesComboBox
            get_SamplesBox(value);
        }

        public void get_SamplesBox(string Music_Name)
        {
            sample_list.Clear();
            
            List<string> samples = new List<string>();
            if (Music_Name == null)
            {
                SamplesBox.ItemsSource = samples;
                return;
            }
            SqlDataReader result;
            SqlDataAdapter da2 = new SqlDataAdapter();
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            SqlCommand command = new SqlCommand("SELECT * FROM SampleKeeper.setSamples(@a)", cn);
            command.Parameters.AddWithValue("@a", Music_Name);
            result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    sample_list.Add(new Sample(result.GetString(0), result.GetString(3), result.GetTimeSpan(1), result.GetString(2), result.GetInt32(4)));
                }
            }
            for (int i = 0; i < sample_list.Count; i++)
            {
                samples.Add(sample_list[i].nome);
            }
            result.Close();
            SamplesBox.ItemsSource = samples;
        }

        public void fillGenreList() {          
            SqlDataReader result;
            SqlDataAdapter da2 = new SqlDataAdapter();
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            SqlCommand command = new SqlCommand("SELECT * FROM SampleKeeper.getGenres()", cn);
            command.CommandType = CommandType.Text;
            result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    genre_list.Add(new Genre(result.GetInt32(0),result.GetString(1)));
                }
            }
            result.Close();
        }

        private void SamplesBox_Change(object sender, SelectionChangedEventArgs e)
        {
            // ... Get the ComboBox.
            var SamplesBox = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            string value = SamplesBox.SelectedItem as string;
            for (int i = 0; i < sample_list.Count; i++)
            {
                if (sample_list[i].nome == value)
                {
                    SampleCreator.Content = sample_list[i].criador;
                    SampleDuration.Content = sample_list[i].duracao.ToString();
                    SampleType.Content = sample_list[i].tipo;
                }
            }
        }

        public void resetMusicInfo()
        {
            button.Visibility = Visibility.Collapsed;
            SongGenre.Content = null;
            SongDuration.Content = null;
            SampleCreator.Content = null;
            SampleDuration.Content = null;
            SampleType.Content = null;
        }

        public void resetSample()
        {
            SamplesBox.SelectedItem = null;
            SampleCreator.Content = null;
            SampleType.Content = null;
            SampleDuration.Content = null;
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
            if (i < 0) i = album_list.Count - 1; if (j < 0) j = album_list.Count - 1; if (k < 0) k = album_list.Count - 1;
            Main.Source = new BitmapImage(new Uri(album_base_dir + album_list[i].image, UriKind.Relative));
            Right.Source = new BitmapImage(new Uri(album_base_dir + album_list[j].image, UriKind.Relative));
            Left.Source = new BitmapImage(new Uri(album_base_dir + album_list[k].image, UriKind.Relative));
            SetInfo(album_list[i]);
            resetMusicInfo();
        }

        private void ButtonSlideLeft_Click(object sender, RoutedEventArgs e)
        {
            i--; j--; k--;
            if (i < 0) i = album_list.Count - 1; if (j < 0) j = album_list.Count - 1; if (k < 0) k = album_list.Count - 1;
            Main.Source = new BitmapImage(new Uri(album_base_dir + album_list[i].image, UriKind.Relative));
            Left.Source = new BitmapImage(new Uri(album_base_dir + album_list[j].image, UriKind.Relative));
            Right.Source = new BitmapImage(new Uri(album_base_dir + album_list[k].image, UriKind.Relative));
            SetInfo(album_list[i]);
            resetMusicInfo();
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

        private void PlaylistSelected(object sender, RoutedEventArgs e)
        {
            PlaylistWindow win = new PlaylistWindow(user, userid);
            win.Show();
            this.Close();
        }

        private void RemoveSong(object sender, RoutedEventArgs e)
        {
            string isrc_sel = "";
            for (i = 0; i < music_list.Count; i++)
            {
                if (music_list[i].nome == SongsBox.SelectedItem.ToString())
                {
                    isrc_sel = music_list[i].isrc;
                }
            }
            SongsBox.SelectedItem.ToString();

            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("SampleKeeper.remove_music", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ISRC", isrc_sel);
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
            button.Visibility = Visibility.Collapsed;
            resetMusicInfo();
            set_SongsBox(c.currentAlbum);
        }

        private void NewAlbum(object sender, RoutedEventArgs e)
        {
            MainGrid.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;

            NewAlbum win = new NewAlbum();
            win.ShowDialog();

            PopulateAlbums();
            Splash.Visibility = Visibility.Collapsed;
            MainGrid.Effect = null;            
        }

        public void NewSong(object sender, RoutedEventArgs e)
        {
            MainGrid.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;

            NewSong win = new NewSong(user, userid);
            win.ShowDialog();

            set_SongsBox(c.currentAlbum);
            resetMusicInfo();
            Splash.Visibility = Visibility.Collapsed;
            MainGrid.Effect = null;
        }

        private void NewSample(object sender, RoutedEventArgs e)
        {          

            MainGrid.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;

            NewSample win = new NewSample(c.currentSong, user, userid);
            win.ShowDialog();

            string temp = "";
            for (int i = 0; i < music_list.Count; i++)
            {
                if (music_list[i].isrc == c.currentSong)
                {
                    temp = music_list[i].nome;
                }
            }
            resetSample();
            get_SamplesBox(temp);
            Splash.Visibility = Visibility.Collapsed;
            MainGrid.Effect = null;
        }

        private void RemoveSample(object sender, RoutedEventArgs e)
        {
            int id = 0;
            for (i = 0; i < sample_list.Count; i++)
            {
                if (sample_list[i].nome == SamplesBox.SelectedItem.ToString())
                {
                    id = sample_list[i].id;
                }
            }

            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("SampleKeeper.remove_sample", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sample_ID", id);
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
            string temp = "";
            for (int i = 0; i < music_list.Count; i++)
            {
                if(music_list[i].isrc == c.currentSong)
                {
                    temp = music_list[i].nome;
                }
            }
            resetSample();
            get_SamplesBox(temp);
        }
        private void RemoveAlbum(object sender, RoutedEventArgs e)
        {
            string catnumber = "";
            for (i = 0; i < album_list.Count; i++)
            {
                if (album_list[i].nome == c.currentAlbum)
                {
                    catnumber = album_list[i].catalog_number;
                }
            }
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("SampleKeeper.remove_album", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@catalog_number", catnumber);
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
                SearchWindow window = new SearchWindow(user,userid);
                window.Show();
                this.Close();
            }
        }

        public void Add2Playlist(object sender, RoutedEventArgs e)
        {
            MainGrid.Effect = new BlurEffect();
            Splash.Visibility = Visibility.Visible;

            Music2Playlist win = new Music2Playlist(user, userid, c.currentSong);
            win.ShowDialog();

            Splash.Visibility = Visibility.Collapsed;
            MainGrid.Effect = null;
        }
    }
}
