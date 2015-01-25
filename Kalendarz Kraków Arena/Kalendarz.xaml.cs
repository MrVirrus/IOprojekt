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
    
    

    public partial class Kalendarz : Window
    {
        string[] miesiace = { "", "Stycznia", "Lutego", "Marca", "Kwietnia", "Maja", "Czerwca", "Lipca", "Sierpnia", "Września", "Października", "Listopada", "Grudnia" };

        int dzisiajmiesiac = DateTime.Today.Month;
        int dzisiajrok = DateTime.Today.Year;
        int dzisiajdzien = DateTime.Today.Day;
        int aktualnymiesiac;
        int aktualnyrok;
        int aktualnydzien;

        Rectangle aktualny, temp;
        int iteracja=0;

        public Kalendarz()
        {

            InitializeComponent();
            for (int i = 0; i < 4; i++)
            { for (int j = 0; j < 7; j++){
                iteracja++;
                Rectangle rectangle = new Rectangle() { Name = Convert.ToString("Kwadrat" + iteracja ), Height = 100, Width = 100, Margin = new Thickness(j * 102 + 100, i * 102 + 100, 0, 0), Stroke = new SolidColorBrush(Colors.Black), Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245)), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
                Label numer = new Label() {Name = Convert.ToString("label"+i), Content = iteracja, Margin = new Thickness( j * 102 + 100 , i * 102 + 100, 0, 0), FontSize = 10};
                rectangle.MouseEnter += new MouseEventHandler(RecMouseEnter);
                rectangle.MouseLeave += new MouseEventHandler(RecMouseLeave);
                rectangle.MouseDown += new MouseButtonEventHandler(RecMouseDown);
                myGrid.Children.Add(rectangle);
                myGrid.Children.Add(numer);
                }
            }
            Content = myGrid;
        }

        private void RecMouseEnter(object sender, MouseEventArgs e)
        {
            on((Rectangle)sender);
        }

        private void RecMouseLeave(object sender, MouseEventArgs e)
        {
            off((Rectangle)sender);
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
            wybrany((Rectangle)sender);
        }

        public void wybrany(Rectangle rec)
        {
            // temp - na czerwono
            // aktualny na czarno

            temp = rec;
            temp.Stroke = new SolidColorBrush(Colors.Red);

            if(aktualny != null)
                aktualny.Stroke = new SolidColorBrush(Colors.Black);
            if (aktualny == temp) aktualny.Stroke = new SolidColorBrush(Colors.Red);
            aktualny = temp;

            miesiac.Content = temp.Name ;
            //refresh();
            //aktualny.Stroke = new SolidColorBrush(Colors.Black);
            

        }

        public void on(Rectangle rec)
        {
            rec.Fill = new SolidColorBrush(Color.FromRgb(222,222,222));
        }

        public void off(Rectangle rec)
        {
            rec.Fill = new SolidColorBrush(Color.FromRgb(244,244,245));
        } 
        

        public void refresh()
        {

            miesiac.Content = aktualnydzien + " " + miesiace[aktualnymiesiac] + "  " + aktualnyrok; 

        }

        public void ustawddzien(Rectangle rec)
        {

            aktualnydzien = Int32.Parse(char.ToString(rec.Name[1]));

        }

        
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            aktualnymiesiac +=1;
            if (aktualnymiesiac == 13)
            {
                aktualnymiesiac = 1;
                aktualnyrok += 1;
            }

            refresh();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            aktualnymiesiac = dzisiajmiesiac;
            aktualnyrok = dzisiajrok;
            aktualnydzien = dzisiajdzien;
            refresh();
        }

        private void Rectangle_MouseDown_1(object sender, MouseButtonEventArgs e)
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
        


        
       
    }
}
