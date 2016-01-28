using System;

namespace CefGlueWinForms.EmbeddedBrowser.Events
{
    public class TitleChangedEventArgs : EventArgs
    {
        public TitleChangedEventArgs(string title)
        {
            Title = title;
        }

        public string Title { get; private set; }
    }
}
