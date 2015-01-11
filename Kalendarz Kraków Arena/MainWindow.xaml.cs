using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
namespace Kalendarz_Kraków_Arena
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Console.CapsLock)
                capswarn.Visibility = Visibility.Visible;
            else
                capswarn.Visibility = Visibility.Hidden;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Console.CapsLock)
                capswarn.Visibility = Visibility.Visible;
            else
                capswarn.Visibility = Visibility.Hidden;
            warn.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {



                MySqlConnection conn;
                string cn = "server=89.68.24.235; user id=user; password='Spectro2005'; database=ioproj";
                conn = new MySql.Data.MySqlClient.MySqlConnection(cn);
                MySqlConnection.ClearPool(conn);
                conn.Open();

                
                MySqlCommand filmsCommand = new MySqlCommand("SELECT * FROM uzytkownicy WHERE Login='"+login.Text+"';", conn);

                MySqlDataReader reader = filmsCommand.ExecuteReader();
                string passs = "";
                string hashedPass = getHashSha256(pass.Password);
                while (reader.Read())
                {
                   
                    passs = reader.GetString("Haslo");
                    //name += reader.GetString("model");
                }

                if (passs == hashedPass)
                {
                    conn.Close();
                    Window Kalendarz = new Kalendarz();
                    Kalendarz.Show();
                    this.Close();
                }
                else
                {
                    warn.Visibility = Visibility.Visible;
                }




        }
    }
}
