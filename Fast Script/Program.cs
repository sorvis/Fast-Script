using Orvis.Framework.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Fast_Script
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            ProcessWatcher.StartWatch();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new MainWindow());
            }
            catch (Exception exception)
            {
                ExceptionHandler.ShowAndLogException(exception);
            }
        }
    }
}
