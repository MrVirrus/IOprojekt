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
    /// Interaction logic for DodajWydarzenie.xaml
    /// </summary>
    public partial class ZobaczWydarzenie : Window
    {
        private List<Event> Eventy;

        public ZobaczWydarzenie(List<Event> Eventy)
        {
            InitializeComponent();
            this.Eventy = Eventy;
            RysujEventy();
        }

        private void RysujEventy()
        {
            if (Eventy.Count == 0)
            {
                Label BrakWydarzenia = new Label() { Name = "BrakWydarzenia", Content = "Żadne wydarzenie\nnie jest przypisane\ndo tego dnia.", FontSize = 30, HorizontalAlignment=HorizontalAlignment.Center, VerticalAlignment=VerticalAlignment.Center };
                DodajWydarzenieGrid.Children.Add(BrakWydarzenia);
            }
            else
            {
                for (int i = 0; i < Eventy.Count; i++)
                {
                    double mnoznik = i * 200;
                    double Y = mnoznik + 200;

                    Label IDOrganizatora = new Label() { Name = "IDOrganizatora", Content = "IDOrganizatora : " + Eventy.ElementAt(i).IDOrganizatora, Margin = new Thickness(0, mnoznik,0,0), FontSize = 20 };
                    Label StanyRezerwacji = new Label() { Name = "StanyRezerwacji", Content = "Stan rezerwacji : " + Eventy.ElementAt(i).StanRezerwacji, Margin = new Thickness(0, mnoznik + 20, 0, 0), FontSize = 20 };
                    Label Od = new Label() { Name = "Od", Content = "Data OD : " + Eventy.ElementAt(i).Od.ToString("yyyy-MM-dd"), Margin = new Thickness(0, mnoznik + 40, 0, 0), FontSize = 20 };
                    Label Do = new Label() { Name = "Od", Content = "Data DO : " + Eventy.ElementAt(i).Do.ToString("yyyy-MM-dd"), Margin = new Thickness(0, mnoznik + 60, 0, 0), FontSize = 20 };
                    Label EventOd = new Label() { Name = "EventOd", Content = "Data Eventu OD : " + Eventy.ElementAt(i).EventOd.ToString("yyyy-MM-dd"), Margin = new Thickness(0, mnoznik + 80, 0, 0), FontSize = 20 };
                    Label EventDo = new Label() { Name = "EventOd", Content = "Data Eventu DO : " + Eventy.ElementAt(i).EventDo.ToString("yyyy-MM-dd"), Margin = new Thickness(0, mnoznik + 100, 0, 0), FontSize = 20 };
                    Label OsobaRezerwujaca = new Label() { Name = "OsobaRezerwujaca", Content = "Osoba rezerwująca : " + Eventy.ElementAt(i).OsobaRezerwujaca, Margin = new Thickness(0, mnoznik + 120, 0, 0), FontSize = 20 };
                    Label NazwaOrganizacji = new Label() { Name = "NazwaOrganizacji", Content = "Nazwa organizacji : " + Eventy.ElementAt(i).NazwaOrganizacji, Margin = new Thickness(0, mnoznik + 140, 0, 0), FontSize = 20 };
                    Line rect = new Line() { Name = "Rect", Y1 = Y, Y2 = Y, X2 = this.Width, Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)), StrokeThickness = 2 };
                    
                    DodajWydarzenieGrid.Children.Add(IDOrganizatora);
                    DodajWydarzenieGrid.Children.Add(StanyRezerwacji);
                    DodajWydarzenieGrid.Children.Add(Od);
                    DodajWydarzenieGrid.Children.Add(Do);
                    DodajWydarzenieGrid.Children.Add(EventOd);
                    DodajWydarzenieGrid.Children.Add(EventDo);
                    DodajWydarzenieGrid.Children.Add(OsobaRezerwujaca);
                    DodajWydarzenieGrid.Children.Add(NazwaOrganizacji);
                    DodajWydarzenieGrid.Children.Add(rect);
                   
                }
                this.Height = Eventy.Count * 230;
            }
        }
    }
}
