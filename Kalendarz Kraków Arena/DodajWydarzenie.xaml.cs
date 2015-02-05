using MySql.Data.MySqlClient;
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

namespace Kalendarz_Kraków_Arena
{
    /// <summary>
    /// Okienko dodające wydarzenie.
    /// </summary>
    public partial class DodajWydarzenie : Window
    {
        public DodajWydarzenie()
        {
            InitializeComponent();
        }

        private void ZapiszWydarzenie(object sender, RoutedEventArgs e)
        {
            //Sprawdzenie, czy dane zostały wypełnione (na razie w jakikolwiek sposób)
            if (StanyRezerwacji.Text.Length < 1 || Organizator.Text.Length < 1 || !Od.SelectedDate.HasValue 
                || !Do.SelectedDate.HasValue || !EventOd.SelectedDate.HasValue || !EventDo.SelectedDate.HasValue
                || OsobaRezerwujaca.Text.Length < 1 || NazwaOrganizatora.Text.Length < 1)
            {
                MessageBox.Show("Wypełnij dane.");
            }
            else
            {

                //Pobieranie danych z kontrolek do zmiennych
                int stanRezerwacji = Int32.Parse(StanyRezerwacji.Text);
                int organizator = Int32.Parse(Organizator.Text);
                string osobaRezerwujaca = OsobaRezerwujaca.Text;
                string nazwaOrganizatora = NazwaOrganizatora.Text;
                DateTime dataOd = DateTime.ParseExact(Od.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dataDo = DateTime.ParseExact(Do.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dataEventOd = DateTime.ParseExact(EventOd.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dataEventDo = DateTime.ParseExact(EventDo.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);


                string connectionString = "server=89.68.24.235; user id=user; password='Spectro2005'; database=ioproj";
                MySqlConnection conn;

                conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                MySqlConnection.ClearPool(conn);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                //Dodawanie rekordów do tabeli "Rezerwacje" za pomocą parametrów
                cmd.CommandText = "INSERT INTO rezerwacje(id_StanyRezerwacji, id_Organizatora, Od, Do, EventOd, EventDo, OsobaRezerwujaca, NazwaOrganizacji) VALUES(?stanrezerwacji, ?organizator, ?od, ?do, ?eventod, ?eventdo, ?osobarez, ?nazwaorg)";
                cmd.Parameters.Add("?stanrezerwacji", MySqlDbType.Int32).Value = stanRezerwacji;
                cmd.Parameters.Add("?organizator", MySqlDbType.Int32).Value = organizator;
                cmd.Parameters.Add("?od", MySqlDbType.DateTime).Value = dataOd;
                cmd.Parameters.Add("?do", MySqlDbType.DateTime).Value = dataDo;
                cmd.Parameters.Add("?eventod", MySqlDbType.DateTime).Value = dataEventOd;
                cmd.Parameters.Add("?eventdo", MySqlDbType.DateTime).Value = dataEventDo;
                cmd.Parameters.Add("?osobarez", MySqlDbType.VarChar).Value = osobaRezerwujaca;
                cmd.Parameters.Add("?nazwaorg", MySqlDbType.VarChar).Value = nazwaOrganizatora;
                cmd.ExecuteNonQuery();

                conn.Close();

                this.Close();
            }
        }
    }
}
