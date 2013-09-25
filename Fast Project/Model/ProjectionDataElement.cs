using Orvis.Framework.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast_Project
{
    public class ProjectionDataElement : ViewModelObject
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
    }
}
