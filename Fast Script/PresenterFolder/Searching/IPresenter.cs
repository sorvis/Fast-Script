using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast_Script.PresenterFolder.Searching
{
    interface IPresenter
    {
        void displayVersesToWebView(ReferenceList list, string boldWords);
        void writeWebView(string page);
        backEndInitializer Backend { get; }
    }
}
