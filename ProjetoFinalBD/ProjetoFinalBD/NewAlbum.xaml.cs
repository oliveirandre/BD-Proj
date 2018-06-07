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
    /// Lógica interna para NewAlbum.xaml
    /// </summary>
    public partial class NewAlbum : Window
    {
        private SqlConnection cn;

        public NewAlbum()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SampleKeeper.add_album", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@catalog_number", albumnumber.Text);
            cmd.Parameters.AddWithValue("@nome", album.Text);
            cmd.Parameters.AddWithValue("@ano", year.Text);
            cmd.Parameters.AddWithValue("@editora", label.Text);
            cmd.Parameters.AddWithValue("@artista", artist.Text);
            cmd.Parameters.AddWithValue("@image", "note.png");

            cmd.Connection = cn;
            if (album.Text == "" || year.Text == "" || label.Text == "" || artist.Text == "" || image.Text == "" || albumnumber.Text == "")
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
