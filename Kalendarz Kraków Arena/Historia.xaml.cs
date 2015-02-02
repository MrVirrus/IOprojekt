using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        

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
                    dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                    da.Fill(dt);                    
                    DataGrid.ItemsSource = dt.DefaultView;
                }
                connection.Close();

                string[] columnNames = dt.Columns.Cast<DataColumn>()//wypelnianie combobox
                                 .Select(x => x.ColumnName)
                                 .ToArray();
                for (int i = 0; i < columnNames.Length; i++)
                {
                    ColumsBox.Items.Add(columnNames[i]);
                }
                ColumsBox.SelectedIndex = 0;
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
                    dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                    da.Fill(dt);                    
                    DataGrid.ItemsSource = dt.DefaultView;
                }
                connection.Close();

                string[] columnNames = dt.Columns.Cast<DataColumn>()//wypelnianie combobox
                                 .Select(x => x.ColumnName)
                                 .ToArray();
                for (int i = 0; i < columnNames.Length; i++)
                {
                    ColumsBox.Items.Add(columnNames[i]);
                }
                ColumsBox.SelectedIndex = 0;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//funkcja odpowiedzialna za filtrowanie
        {
            //(dataGridViewFields.DataSource as DataTable).DefaultView.RowFilter = string.Format("Field = '{0}'", TextBox.Text);           
            dv = new DataView(dt);            
            string klucz;
            string wartosc;

            klucz = ColumsBox.SelectedItem.ToString();
            wartosc = FilterBox.Text;
            
            if (klucz != "" && wartosc != "")//to nie do konca dziala trzeba tutaj dosrac jeszcze duzo warunkow
            {
                MessageBox.Show("" + klucz + " = " + wartosc + "");
                dv.RowFilter = "" + klucz + " = " + wartosc + "";
            }
            //dv.RowFilter = "Login=2";
            DataGrid.ItemsSource = dv;
        }
    }
}
