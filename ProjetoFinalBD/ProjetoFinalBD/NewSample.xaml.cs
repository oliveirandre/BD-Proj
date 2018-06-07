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
    /// Lógica interna para NewSample.xaml
    /// </summary>
    public partial class NewSample : Window
    {
        private SqlConnection cn;
        string currentSong;
        public string user;
        public int userid;
        public NewSample(string currentSong, string user, int userid)
        {
            InitializeComponent();
            this.user = user;
            this.currentSong = currentSong;
            this.userid = userid;
            SearchWindow win = new SearchWindow(user, userid);
            Console.WriteLine(win.c.currentSong);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime timestamp = DateTime.ParseExact(duration.Text, "mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SampleKeeper.add_sample", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nome", sample.Text);
            cmd.Parameters.AddWithValue("@tipo", tipo.Text);
            cmd.Parameters.AddWithValue("@duracao", timestamp);
            cmd.Parameters.AddWithValue("@localizacao", sample.Text + ".mp3");
            cmd.Parameters.AddWithValue("@criador", userid);
            cmd.Parameters.AddWithValue("@musica", currentSong);

            cmd.Connection = cn;
            if (sample.Text == "" || tipo.Text == "" || duration.Text == "")
            {
                hidden.Content = "You must fill in all the fields!";
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
