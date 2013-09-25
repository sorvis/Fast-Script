using Orvis.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast_Project
{
    public class ViewModel:ViewModelObject
    {
        public ViewModel()
        {
            ProjectionDataElementList = new ObservableCollection<ProjectionDataElement>();
        }

        public ObservableCollection<ProjectionDataElement> ProjectionDataElementList { get; set; }

        private ProjectionDataElement _currentSelectedProjectionDataElement;

        public ProjectionDataElement CurrentSelectedProjectionDataElement
        {
            get 
            { 
                return _currentSelectedProjectionDataElement; 
            }
            set 
            { 
                _currentSelectedProjectionDataElement = value;
                OnPropertyChanged("CurrentSelectedProjectionDataElement");
            }
        }

    }
}
