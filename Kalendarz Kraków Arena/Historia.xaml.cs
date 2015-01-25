using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Kalendarz_Kraków_Arena
{
    /// <summary>
    /// Interaction logic for Historia.xaml
    /// </summary>
    public partial class Historia : Window
    {
        public Historia()
        {
            InitializeComponent();
        }

        private void HLog(object sender, RoutedEventArgs e)
        {
            string cn = "server=89.68.24.235; user id=user; password='Spectro2005'; database=ioproj";
            string sql = "SELECT * FROM historialogowania";

            using (MySqlConnection connection = new MySqlConnection(cn))
            {
                connection.Open();
                using (MySqlCommand cmdSel = new MySqlCommand(sql, connection))
                {
                    DataTable dt = new DataTable();                    
                    MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                    da.Fill(dt);                    
                    DataGrid.ItemsSource = dt.DefaultView;
                }
                connection.Close();
            }
        }

        private void HRez(object sender, RoutedEventArgs e)
        {
            string cn = "server=89.68.24.235; user id=user; password='Spectro2005'; database=ioproj";
            string sql = "select * from v_historiarezerwacji2;";

            using (MySqlConnection connection = new MySqlConnection(cn))
            {
                connection.Open();
                using (MySqlCommand cmdSel = new MySqlCommand(sql, connection))
                {
                    DataTable dt = new DataTable();                    
                    MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                    da.Fill(dt);                    
                    DataGrid.ItemsSource = dt.DefaultView;
                }
                connection.Close();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
