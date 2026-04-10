using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Exceptions;
using Microsoft.VisualBasic.Logging;

namespace UI.Exceptions
{
    public static class ExceptionHandler
    {
        public static void Handle(Exception ex)
        {
            Log(ex);

            switch (ex)
            {
                case EntityNotFoundException e:
                    ShowError(e.Message);
                    break;

                default:
                    ShowError("An unexpected error occurred. Please try again later.");
                    break;
            }
        }
        private static void Log(Exception e)
        {
            //log the exception to a file or database here
        }

        private static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
