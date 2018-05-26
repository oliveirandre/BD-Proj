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
    /// Lógica interna para Window3.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        //Zona de variáveis
        private SqlConnection cn;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand();
            String query = "SELECT COUNT(1) FROM SampleKeeper.Account WHERE nickname=@nickname AND password=@password";
            SqlCommand sqlCmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@nickname", txtUsername.Text);
            cmd.Parameters.AddWithValue("@password", txtPassword.Password);
            cmd.Connection = cn;
            int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
            if (txtUsername.Text == "" || txtPassword.Password == "")
            {
                label1.Content = "You must fill in all the fields!";
                label1.Visibility = Visibility.Visible;
            }
            else if (count != 1)
            {
                label1.Content = "Username or Password is incorrect!";
                label1.Visibility = Visibility.Visible;
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
                Window2 win2 = new Window2();
                win2.Show();
                this.Close();
            }            
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            SignupWindow win4 = new SignupWindow();
            win4.Show();
            this.Close();
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
