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
using MySql.Data.MySqlClient;
namespace Kalendarz_Kraków_Arena
{

    public partial class ConnectionSettings : Window
    {
        public static string connectionString = "";
        private static void encode(string s, string n)
        {

            string wynik = "";
            string[] tab = new string[300];
            int seed = 9;
            int j = 0;
            foreach (char i in s)
            {
                if ((int)i % 2 == 0)
                {
                    wynik += (int)i + 9;
                    tab[j] = wynik + "f";
                    wynik = "";
                    j++;
                }
                else
                {
                    wynik += (int)i + 6;
                    tab[j] = wynik + "b";
                    wynik = "";
                    j++;
                }

            }

            System.IO.File.WriteAllLines(@n, tab);
        }
        public static string decode(string n)
        {
            string[] s = System.IO.File.ReadAllLines(@n);
            string wynik = "";
            int integer = 0;
            int seed = 9;
            foreach (string str in s)
            {
                if (str != "")
                {
                    if (str[str.Length - 1] == 'f')
                    {
                        integer = Convert.ToInt16(str.TrimEnd('f'));
                        wynik += (char)(integer - 9);
                    }
                    else
                    {
                        integer = Convert.ToInt16(str.TrimEnd('b'));
                        if (integer % 2 == 0) seed = 9; else seed = 6;
                        wynik += (char)(integer - 6);
                    }
                }
            }
            return wynik;
        }
        private static string[] dane = new string[4];
        public ConnectionSettings()
        {

            InitializeComponent();
            connectionString = decode("any.txt");
            if (connectionString != "")
            {
                connectionString = connectionString.Replace("server=", "");
                connectionString = connectionString.Replace(" user id=", "");
                connectionString = connectionString.Replace(" password='", "");
                connectionString = connectionString.Replace("';", ";");
                connectionString = connectionString.Replace(" database=", "");
                string[] dane = connectionString.Split(';');


            host.Text = dane[0];
            login.Text = dane[1];
            pwd.Password = dane[2];
            db.Text = dane[3];
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            connectionString = "server=" + host.Text + "; user id=" + login.Text + "; password='" + pwd.Password + "'; database=" + db.Text+";";
            MySqlConnection conn;

            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                encode(connectionString, "any.txt");
                CS.Content = "Połączono";
            }
            catch (MySqlException ex)
            {
                CS.Content="Brak połączenia z bazą";
            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
