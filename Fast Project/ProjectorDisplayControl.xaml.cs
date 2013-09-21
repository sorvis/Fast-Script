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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fast_Project
{
    /// <summary>
    /// Interaction logic for ProjectorDisplayControl.xaml
    /// </summary>
    public partial class ProjectorDisplayControl : UserControl
    {
        static ProjectorDisplayControl()
        {
            ProjectedDataProperty = DependencyProperty.Register("ProjectedData", typeof(ProjectionDataElement), typeof(ProjectorDisplayControl),
                new PropertyMetadata(new PropertyChangedCallback((objectInstance, arguments) =>
                {
                    ProjectorDisplayControl projectorDisplayControl = (ProjectorDisplayControl)objectInstance;
                    projectorDisplayControl._projectedData = (ProjectionDataElement)arguments.NewValue;
                })));
        }

        public ProjectorDisplayControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ProjectedDataProperty;

        public ProjectionDataElement ProjectedData
        {
            get
            {
                return (ProjectionDataElement)GetValue(ProjectedDataProperty);
            }
            set
            {
                SetValue(ProjectedDataProperty, value);
            }
        }


        private ProjectionDataElement _projectedData
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
    }
}
