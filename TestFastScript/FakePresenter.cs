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
        public BackEndInitializer Backend { get; set; }
        public void WriteWebView(string page)
        {

        }
         public void DisplayVersesToWebView(ReferenceItems list, string boldWords)
         {

         }
        
    }
}
