using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast_Script.PresenterFolder.Searching
{
    interface IPresenter
    {
        void DisplayVersesToWebView(ReferenceItems list, string boldWords);
        void WriteWebView(string page);
        BackEndInitializer Backend { get; }
    }
}
