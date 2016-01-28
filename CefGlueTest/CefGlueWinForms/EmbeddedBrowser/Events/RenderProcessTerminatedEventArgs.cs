using System;
using Xilium.CefGlue;

namespace CefGlueWinForms.EmbeddedBrowser.Events
{
    public class RenderProcessTerminatedEventArgs : EventArgs
    {
        public RenderProcessTerminatedEventArgs(CefTerminationStatus status)
        {
            Status = status;
        }

        public CefTerminationStatus Status { get; private set; }
    }
}
