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
    /// Interaction logic for O_programie.xaml
    /// </summary>
    public partial class O_programie : Window
    {
        public O_programie()
        {
            InitializeComponent();
            PodstawTekst();
        }
        private void PodstawTekst()
        {
            Zawartosc.Text = "\n";
            Zawartosc.Text += "Twórcy:\n";
            Zawartosc.Text += "Dorian N.\n";
            Zawartosc.Text += "Przemysław K.\n";
            Zawartosc.Text += "Tomasz P.\n";
        }
        private void Zamknij(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
