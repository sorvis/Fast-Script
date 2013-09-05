using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast_Project
{
    class ProjectionDataElement : INotifyPropertyChanged
    {
        private String _textBody;
        public String TextBody
        { 
            get 
            { 
                return _textBody; 
            } 
            set 
            { 
                _textBody = value;
                OnPropertyChanged("TextBody");
            } 
        }

        // Declare the event 
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
