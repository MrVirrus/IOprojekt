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

    public class Event
    {
        #region Zmapowane pola z tabeli Rezerwacje
        public DateTime Od { get; set; }
        public DateTime Do { get; set; }
        public DateTime EventOd { get; set; }
        public DateTime EventDo { get; set; }
        public int IDOrganizatora { get; set; }
        public int StanRezerwacji { get; set; }
        public string OsobaRezerwujaca { get; set; }
        public string NazwaOrganizacji { get; set; }
        #endregion
        public Event(int IDOrganizatora, int StanRezerwacji, DateTime Od, DateTime Do, DateTime EventOd, DateTime EventDo, string OsobaRezerwujaca, string NazwaOrganizacji){
            this.IDOrganizatora = IDOrganizatora;
            this.StanRezerwacji = StanRezerwacji;
            this.Od = Od;
            this.Do = Do;
            this.EventOd = EventOd;
            this.EventDo = EventDo;
            this.OsobaRezerwujaca = OsobaRezerwujaca;
            this.NazwaOrganizacji = NazwaOrganizacji;
        }
    }

    public partial class Kalendarz : Window
    {
        readonly string[] miesiace = { "", "Stycznia", "Lutego", "Marca", "Kwietnia", "Maja", "Czerwca", "Lipca", "Sierpnia", "Września", "Października", "Listopada", "Grudnia" };

        int dzisiajmiesiac = DateTime.Today.Month;
        int dzisiajrok = DateTime.Today.Year;
        int dzisiajdzien = DateTime.Today.Day;
        int aktualnymiesiac;
        int aktualnyrok;
        int aktualnydzien;
        List<Event> Eventy = new List<Event>();
  
        DateTime AktualnaData = System.DateTime.Now;

        Canvas aktualny, temp;
        public Kalendarz()
        {
            PobierzDane();
            InitializeComponent();
            RysujKalendarz(AktualnaData.Year, AktualnaData.Month);
        }

        private void PobierzDane()
        {
            string connectionString = "server=89.68.24.235; user id=user; password='Spectro2005'; database=ioproj";
            MySqlConnection conn;

            conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            MySqlConnection.ClearPool(conn);
            conn.Open();
            //Nie działa czytanie procedury
            //MySqlCommand filmsCommand = new MySqlCommand("call p_GetDates(" + Rok + ", " + Miesiac + ");", conn)
            MySqlCommand filmsCommand = new MySqlCommand("SELECT * FROM rezerwacje", conn);
            MySqlDataReader reader = filmsCommand.ExecuteReader();

            DateTime Od, Do, EventOd, EventDo;
            int idOrganizatora, idStanRezerwacji;
            string OsobaRezerwujaca, NazwaOrganizacji;

            while (reader.Read())
            {
                idOrganizatora = reader.GetInt32("id_Organizatora");
                idStanRezerwacji = reader.GetInt32("id_StanyRezerwacji");
                Od = reader.GetDateTime("Od");
                Do = reader.GetDateTime("Do");
                EventOd = reader.GetDateTime("EventOd");
                EventDo = reader.GetDateTime("EventDo");
                OsobaRezerwujaca = reader.GetString("OsobaRezerwujaca");
                NazwaOrganizacji = reader.GetString("NazwaOrganizacji");
                Eventy.Add(new Event(idOrganizatora, idStanRezerwacji, Od, Do, EventOd, EventDo, OsobaRezerwujaca, NazwaOrganizacji));
            }
            conn.Close();
        }

        private void RysujKalendarz(int Rok, int Miesiac)
        {
            List<Canvas> Kafelki = new List<Canvas>();
            //Obliczamy ilość dni w aktualnym miesiącu
            int dni = DateTime.DaysInMonth(Rok, Miesiac);
            //Ilość wierszy w danym miesiącu -> jeden tydzień
            int liczba_wierszy = 1;
            //Tworzymy kalendarz
            for (int i = 0; i < dni; i++)
            {
                //Tworzymy border
                Border border = new Border();
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1);
                border.Width = 100;
                border.Height = 100;
                //Tworzymy Canvas
                Canvas Canvas = new Canvas() { Name =  "Data" + (1+i).ToString("D2")+Miesiac.ToString("D2")+Rok.ToString() , Height = 100, Width = 100, Margin = new Thickness(i % 7 * 102 + 139, liczba_wierszy * 102 + 40, 0, 0), Background = new SolidColorBrush(Color.FromRgb(244, 244, 245)), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
                //Dodajemy border
                Canvas.Children.Add(border);
                Kafelki.Add(Canvas);
                //Tworzymy label
                Label numer = new Label() { Name = Convert.ToString("label" + i), Content = i + 1, FontSize = 10 };
                //Dodajemy do obiektu CANVAS
                Canvas.Children.Add(numer);
                //Eventy
                Canvas.MouseEnter += new MouseEventHandler(RecMouseEnter);
                Canvas.MouseLeave += new MouseEventHandler(RecMouseLeave);
                Canvas.MouseDown += new MouseButtonEventHandler(RecMouseDown);
                //Dodajemy element do Grid'a
                KafelkiKalendarz.Children.Add(Canvas);
                //Sprawdzamy, czy dzień jest ostatni (liczymy od 0, dlatego 6 jest ostatni)
                if (i % 7 == 6)
                {
                    liczba_wierszy += 1;
                }
                
            }
            RysujKwadraty(Kafelki, Rok, Miesiac);
        }

        private void RecMouseEnter(object sender, MouseEventArgs e)
        {
            on((Canvas)sender);
        }

        private void RecMouseLeave(object sender, MouseEventArgs e)
        {
            off((Canvas)sender);
        }
        private void Zamknij(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void O_programie(object sender, RoutedEventArgs e)
        {
            Window pom = new O_programie();
            pom.Show();
        }

        private void RecMouseDown(object sender, MouseEventArgs e)
        {
            wybrany((Canvas)sender);
        }

        private void RysujKwadraty(List<Canvas> Kafelki, int Rok, int Miesiac)
        {
            foreach(Event e in Eventy)
            {
                foreach (Canvas c in Kafelki)
                {
                    string Nazwa = c.Name.Substring(4,8);
                    DateTime NowaData = DateTime.ParseExact(Nazwa.Substring(4, 4) + "-" + Nazwa.Substring(2, 2) + "-" + Nazwa.Substring(0, 2), "yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture);
                    if (e.Od.Date <= NowaData.Date && NowaData.Date <= e.Do.Date)
                    {
                        SolidColorBrush myBackground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                        if (e.IDOrganizatora == 1)
                        {
                            myBackground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                            //Tworzymy border
                            Border border = new Border();
                            border.BorderBrush = Brushes.Black;
                            border.BorderThickness = new Thickness(1);
                            border.Width = 20;
                            border.Height = 20;
                            //Tworzymy Canvas
                            Canvas Canvas = new Canvas() { Name = "Event" + e.IDOrganizatora + Nazwa, Height = 20, Width = 20, Margin = new Thickness(20, 20, 0, 0), Background = myBackground, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
                            Canvas.Children.Add(border);
                            c.Children.Add(Canvas);
                        }
                        else if (e.IDOrganizatora == 2)
                        {
                            myBackground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                            //Tworzymy border
                            Border border = new Border();
                            border.BorderBrush = Brushes.Black;
                            border.BorderThickness = new Thickness(1);
                            border.Width = 20;
                            border.Height = 20;
                            //Tworzymy Canvas
                            Canvas Canvas = new Canvas() { Name = "Event" + e.IDOrganizatora + Nazwa, Height = 20, Width = 20, Margin = new Thickness(50, 20, 0, 0), Background = myBackground, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
                            Canvas.Children.Add(border);
                            c.Children.Add(Canvas);
                        }
                        else if (e.IDOrganizatora == 3)
                        {
                            myBackground = new SolidColorBrush(Color.FromRgb(0, 0, 255));
                            //Tworzymy border
                            Border border = new Border();
                            border.BorderBrush = Brushes.Black;
                            border.BorderThickness = new Thickness(1);
                            border.Width = 20;
                            border.Height = 20;
                            //Tworzymy Canvas
                            Canvas Canvas = new Canvas() { Name = "Event" + e.IDOrganizatora + Nazwa, Height = 20, Width = 20, Margin = new Thickness(20, 50, 0, 0), Background = myBackground, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
                            Canvas.Children.Add(border);
                            c.Children.Add(Canvas);
                        }
                        else if (e.IDOrganizatora == 4)
                        {
                            myBackground = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            //Tworzymy border
                            Border border = new Border();
                            border.BorderBrush = Brushes.Black;
                            border.BorderThickness = new Thickness(1);
                            border.Width = 20;
                            border.Height = 20;
                            //Tworzymy Canvas
                            Canvas Canvas = new Canvas() { Name = "Event" + e.IDOrganizatora + Nazwa, Height = 20, Width = 20, Margin = new Thickness(50, 50, 0, 0), Background = myBackground, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
                            Canvas.Children.Add(border);
                            c.Children.Add(Canvas);
                        }
                    }
                }

            }
        }

        public void wybrany(Canvas rec)
        {
            // temp - na czerwono
            // aktualny na czarno

            temp = rec;

            Border myBorder = temp.Children.OfType<Border>().FirstOrDefault();
            if (myBorder != null)
            {
                temp.Children.Remove(myBorder);
                Border newBorder = new Border();
                newBorder.BorderBrush = Brushes.Red;
                newBorder.BorderThickness = new Thickness(1);
                newBorder.Width = 100;
                newBorder.Height = 100;
                temp.Children.Add(newBorder);
            }

            if (aktualny != null && aktualny != rec)
            {
                Border myBorder1 = aktualny.Children.OfType<Border>().First();

                if (myBorder1 != null)
                {
                    aktualny.Children.Remove(myBorder1);
                    Border newBorder1 = new Border();
                    newBorder1.BorderBrush = Brushes.Black;
                    newBorder1.BorderThickness = new Thickness(1);
                    newBorder1.Width = 100;
                    newBorder1.Height = 100;
                    aktualny.Children.Add(newBorder1);
                }
            }

            Label myLabel = rec.Children.OfType<Label>().First();
            Odswiez(Convert.ToInt32(myLabel.Content));

            aktualny = temp;

            List<Event> eventyKafelka = new List<Event>();

            foreach (Event e in Eventy)
            {
                string Nazwa = rec.Name.Substring(4,8);
                    DateTime NowaData = DateTime.ParseExact(Nazwa.Substring(4, 4) + "-" + Nazwa.Substring(2, 2) + "-" + Nazwa.Substring(0, 2), "yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture);
                    if (e.Od.Date <= NowaData.Date && NowaData.Date <= e.Do.Date)
                    {
                        eventyKafelka.Add(e);
                    }
            }

            Window okno = new ZobaczWydarzenie(eventyKafelka);
            okno.Show();
        }

        public void on(Canvas rec)
        {
            rec.Background = new SolidColorBrush(Color.FromRgb(222, 222, 222));
        }

        public void off(Canvas rec)
        {
            rec.Background = new SolidColorBrush(Color.FromRgb(244,244,245));
        } 
        

        public void refresh()
        {

            miesiac.Content = aktualnydzien + " " + miesiace[aktualnymiesiac] + " " + aktualnyrok; 

        }

        public void ustawddzien(Canvas rec)
        {

            aktualnydzien = Int32.Parse(char.ToString(rec.Name[1]));

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Odswiez();
        }

        private void Odswiez()
        {
            aktualnymiesiac = dzisiajmiesiac;
            aktualnyrok = dzisiajrok;
            aktualnydzien = dzisiajdzien;
            refresh();
        }

        private void Odswiez(DateTime data)
        {
            aktualnymiesiac = data.Month;
            aktualnyrok = data.Year;
            aktualnydzien = data.Day;
            refresh();
        }

        private void Odswiez(int Dzien)
        {
            aktualnymiesiac = AktualnaData.Month;
            aktualnyrok = AktualnaData.Year;
            aktualnydzien = Dzien;
            refresh();
        }

        private void Canvas_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            aktualnymiesiac -= 1;
            if (aktualnymiesiac == 0)
            {
                aktualnymiesiac = 12;
                aktualnyrok -= 1;
            }
            refresh();     
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (AdminMenu.Visibility == System.Windows.Visibility.Visible) AdminMenu.Visibility = System.Windows.Visibility.Hidden;
            else AdminMenu.Visibility = System.Windows.Visibility.Visible;
        }
        private void Historia_OpenWindow(object sender, RoutedEventArgs e)
        {
            Window historia = new Historia();
            historia.Show();
        }

        private void DodajWydarzenie_OpenWindow(object sender, RoutedEventArgs e)
        {
            Window DodajWydarzenie = new DodajWydarzenie();
            DodajWydarzenie.Closed += OdswiezKalendarz;
            DodajWydarzenie.Show();
        }

        private void OdswiezKalendarz(object sender, EventArgs e)
        {
            Eventy.Clear();
            PobierzDane();
            KafelkiKalendarz.Children.Clear();
            RysujKalendarz(AktualnaData.Year, AktualnaData.Month);
            Odswiez(AktualnaData);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            KafelkiKalendarz.Children.Clear();
            DateTime NowaData = AktualnaData.AddMonths(-1);
            RysujKalendarz(NowaData.Year, NowaData.Month);
            Odswiez(NowaData);
            AktualnaData = NowaData;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            KafelkiKalendarz.Children.Clear();
            DateTime NowaData = AktualnaData.AddMonths(1);
            RysujKalendarz(NowaData.Year, NowaData.Month);
            Odswiez(NowaData);
            AktualnaData = NowaData;
        }
    }
}
