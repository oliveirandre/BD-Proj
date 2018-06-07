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
    /// Lógica interna para Window4.xaml
    /// </summary>
    public partial class SignupWindow : Window
    {
        //Zona de variáveis
        private SqlConnection cn;

        public SignupWindow()
        {
            InitializeComponent();
        }

        private void ButtonSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("SampleKeeper.add_account",cn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@username", txtUsername.Text);
            cmd.Parameters.AddWithValue("@password", txtPassword.Password);

            cmd.Connection = cn;

            if (txtEmail.Text == "" || txtUsername.Text == "" || txtPassword.Password == "" || txtPassword2.Password == "") {
                label1.Content = "You must fill in all the fields!";
                label1.Visibility = Visibility.Visible;
            }

            /*else if (returnvalue.equals1)
            {
                label1.Content = "There is already an account with that username!";
            }*/

            else if (txtPassword.Password != txtPassword2.Password)
            {
                label1.Content = "Passwords don't match!";
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

                LoginWindow win = new LoginWindow();
                win.Show();
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
