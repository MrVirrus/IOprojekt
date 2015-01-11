﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            Window Kalendarz = new Kalendarz();

            Kalendarz.Show();
        }
    }
}
