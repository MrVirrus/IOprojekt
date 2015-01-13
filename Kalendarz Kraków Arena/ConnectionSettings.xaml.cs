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
        public static string connectionStringBackup = "";
        DBconnect connection = new DBconnect();

        private static string[] dane = new string[4];
        public ConnectionSettings()
        {

            InitializeComponent();

            connectionString = connection.decode();
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
            connection.encode(connectionString);
            MySqlConnection conn;

            if(connection.isConnected())
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                CS.Content = "Połączono";
                connectionStringBackup = connectionString;
            }
            else
            {
                CS.Content = "Brak połączenia z bazą";
                if(connectionStringBackup!="")  connection.encode(connectionStringBackup);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
