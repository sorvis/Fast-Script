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

namespace Fast_Project
{
    /// <summary>
    /// Interaction logic for DisplayWindow.xaml
    /// </summary>
    public partial class DisplayWindow : Window
    {
        public DisplayWindow()
        {
            InitializeComponent();
        }

        public ProjectionDataElement ProjectedData
        {
            get
            {
                return this.DataContext as ProjectionDataElement;
            }
            set
            {
                this.DataContext = value;
            }
        }

        private void setScreen()
        {
            var projectorScreen = ScreenHandler.GetProjectorScreen();
            var currentScreen = ScreenHandler.GetCurrentScreen(this);
            if (projectorScreen.DeviceName != currentScreen.DeviceName)
            {
                this.WindowState = WindowState.Normal;
                this.Left = projectorScreen.WorkingArea.Left;
                this.Top = projectorScreen.WorkingArea.Top;
                this.Width = projectorScreen.WorkingArea.Width;
                this.Height = projectorScreen.WorkingArea.Height;
                this.WindowState = WindowState.Maximized;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setScreen();
        }
    }
}
