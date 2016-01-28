using System;

namespace CefGlueWinForms.EmbeddedBrowser.Events
{
    public sealed class StatusMessageEventArgs : EventArgs
    {
        private readonly string value;

        public StatusMessageEventArgs(string value)
        {
            this.value = value;
        }

        public string Value { get { return value; } }
    }
}
