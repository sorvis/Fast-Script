using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            ProjectedDataList = new ObservableCollection<ProjectionDataElement>();
            ProjectedDataList.Add(new ProjectionDataElement() { TextBody = "testsf" });
        }


        private ObservableCollection<ProjectionDataElement> ProjectedDataList
        {
            get
            {
                return this.DataContext as ObservableCollection<ProjectionDataElement>;
            }
            set
            {
                this.DataContext = value;
            }
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
                _display.ProjectedData = ProjectedDataList[0]; // TODO move line to XAML for current selected ProjectionDataElement
                _display.Show();
            }
        }

        private DisplayWindow _display;

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_display != null)
            {
                _display.Close();
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectionDataElement projectedData = new ProjectionDataElement();
            ProjectedDataList.Add(projectedData);
            this.projectedDataListBox.SelectedItem = projectedData;
        }
    }
}
