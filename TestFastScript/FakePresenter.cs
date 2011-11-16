using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fast_Script.PresenterFolder;
using Fast_Script;

namespace TestFastScript
{
    class FakePresenter : Fast_Script.PresenterFolder.Searching.IPresenter
    {
        public backEndInitializer Backend { get; set; }
        public void writeWebView(string page)
        {

        }
         public void displayVersesToWebView(ReferenceList list, string boldWords)
         {

         }
        
    }
}
