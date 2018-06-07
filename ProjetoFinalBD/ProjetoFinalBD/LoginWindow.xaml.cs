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
            int userID = 0;
            if(!verifySGBDConnection())
            {
                return;
            }
            SqlDataAdapter da2 = new SqlDataAdapter();            
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            SqlCommand command = new SqlCommand("SELECT SampleKeeper.getLogin(@nickname, @password)", cn);
            command.Parameters.AddWithValue("@nickname", txtUsername.Text);
            command.Parameters.AddWithValue("@password", txtPassword.Password);            
            userID = (int)command.ExecuteScalar();
            
            if(userID != 0)
            {
                cn.Close();
                SearchWindow win2 = new SearchWindow(txtUsername.Text, userID);
                win2.Show();
                this.Close();
            }
                       
            else
            {
                label1.Content = "Wrong Username and/or Password!";
                label1.Visibility = Visibility.Visible;
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
