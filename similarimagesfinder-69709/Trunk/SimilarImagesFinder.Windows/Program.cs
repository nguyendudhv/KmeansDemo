namespace EyeOpen.SimilarImagesFinder.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using EyeOpen.SimilarImagesFinder.Windows.Forms;

    internal static class Program
    {
        private static IList<IErrorSubscriber> errorsSubscribers;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            errorsSubscribers = new List<IErrorSubscriber>();

            Application.Run(new MainForm());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException(errorsSubscribers, e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(errorsSubscribers, (Exception)e.ExceptionObject);
        }

        private static void HandleException(IEnumerable<IErrorSubscriber> subscribers, Exception lastError)
        {
            try
            {
                foreach (var subscriber in subscribers)
                {
                    subscriber.RegisterError(lastError);
                }
            }
            catch
            {
            }
        }
    }
}