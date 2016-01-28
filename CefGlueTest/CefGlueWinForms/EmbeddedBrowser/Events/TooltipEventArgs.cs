using System;

namespace CefGlueWinForms.EmbeddedBrowser.Events
{
    public class TooltipEventArgs : EventArgs
    {
        public TooltipEventArgs(string text)
        {
            Text = text;
        }

        public string Text { get; private set; }
        public bool Handled { get; set; }
    }
}
