using System;
using System.Globalization;
using Xilium.CefGlue;

namespace CefGlueWinForms.EmbeddedBrowser
{
    public class CefQuickInitializer
    {
        public static int QuickInit(string[] args)
        {
            try
            {
                CefRuntime.Load();
            }
            catch (DllNotFoundException ex)
            {
                throw new ApplicationException("ex.Message");
                return 1;
            }
            catch (CefRuntimeException ex)
            {
                throw new ApplicationException("ex.Message");
                return 2;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ex.Message");
                return 3;
            }

            var mainArgs = new CefMainArgs(args);
            var app = new MyCefApp();

            IntPtr windowsSandboxHandle = IntPtr.Zero;

            var exitCode = CefRuntime.ExecuteProcess(mainArgs, app, windowsSandboxHandle);
            if (exitCode != -1)
                return exitCode;

            var settings = new CefSettings
            {
                SingleProcess = false,
                MultiThreadedMessageLoop = true,
                LogSeverity = CefLogSeverity.Disable,
                LogFile = "CefGlue.log",
                Locale = CultureInfo.CurrentCulture.Name,
                 
            };


            settings.RemoteDebuggingPort = 2035;

            CefRuntime.Initialize(mainArgs, settings, app, windowsSandboxHandle);

            return 0;
        }
    }
}
