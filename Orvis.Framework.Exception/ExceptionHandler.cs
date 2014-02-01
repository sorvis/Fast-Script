using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orvis.Framework.Exception
{
    public class ExceptionHandler
    {
        public static void ShowAndLogException(System.Exception exception)
        {
            Logging.LogException(exception);
            MessageBox.Show(getFormattedException(exception), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static String getFormattedException(System.Exception exception)
        {
            if (exception != null)
            {
                String exceptionMessage = getFormattedException(exception.InnerException);
                if (!exceptionMessage.Contains(exception.Message))
                {
                    exceptionMessage += (String.IsNullOrWhiteSpace(exceptionMessage) ? String.Empty : Environment.NewLine) + exception.Message;
                }
                return exceptionMessage;
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
