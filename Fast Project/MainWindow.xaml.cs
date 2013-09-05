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

namespace Fast_Project
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

        private void ShowDisplayButton_Click(object sender, RoutedEventArgs e)
        {
            if (_display != null && _display.IsVisible)
            {
                _display.Close();
            }
            else
            {
                _display =  new DisplayWindow();
                _display.Show();
            }
        }

        private DisplayWindow _display;
    }
}
