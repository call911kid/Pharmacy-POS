using UI.Exceptions;

namespace UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.ThreadException += (sender, e) =>
                ExceptionHandler.Handle(e.Exception);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                ExceptionHandler.Handle(e.ExceptionObject as Exception);

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}