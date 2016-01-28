using System;
using Xilium.CefGlue;

namespace CefGlueWinForms.EmbeddedBrowser
{
    public class CefLifeCycle : IDisposable
    {
        public CefLifeCycle()
        {
            if (CefQuickInitializer.QuickInit(new string[0]) != 0)
            {
                throw new Exception("Failed to init CEF, return code was not 0");
            }
        }

        public void Dispose()
        {
            CefRuntime.Shutdown();
        }
    }
}
