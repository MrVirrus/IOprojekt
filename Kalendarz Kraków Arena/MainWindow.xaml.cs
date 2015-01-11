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

namespace Kalendarz_Kraków_Arena
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


<<<<<<< Updated upstream
=======

           
>>>>>>> Stashed changes
                MySqlConnection conn;
                string cn = "server=89.68.24.235; user id=user; password='Spectro2005'; database=ioproj";
                conn = new MySql.Data.MySqlClient.MySqlConnection(cn);
                MySqlConnection.ClearPool(conn);
                conn.Open();
<<<<<<< Updated upstream
                /*
                MySqlCommand filmsCommand = new MySqlCommand("SELECT * FROM komputer", conn);

                MySqlDataReader reader = filmsCommand.ExecuteReader();

                string name = "";
                int id = 0;

                while (reader.Read())
                {
                    id += reader.GetInt32("id_komp");
                    name += reader.GetString("model");
                }
                
                conn.Close();
                 */
=======

>>>>>>> Stashed changes
                capswarn.Content = conn.ServerVersion; 

            Window Kalendarz = new Kalendarz();

            Kalendarz.Show();
        }
    }
}
