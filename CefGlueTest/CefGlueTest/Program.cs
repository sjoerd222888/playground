using System;
using System.Windows.Forms;
using CefGlueWinForms.EmbeddedBrowser;
using Xilium.CefGlue;

namespace CefGlueTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            using (CefLifeCycle lifeCycle = new CefLifeCycle())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new EmbeddedBrowserForm());    
            }
        }
    }
}
