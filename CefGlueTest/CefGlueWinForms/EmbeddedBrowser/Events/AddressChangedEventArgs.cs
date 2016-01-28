using System;
using Xilium.CefGlue;

namespace CefGlueWinForms.EmbeddedBrowser.Events
{
    public class AddressChangedEventArgs : EventArgs
    {
        public AddressChangedEventArgs(CefFrame frame, string address)
        {
            Address = address;
            Frame = frame;
        }

        public string Address { get; private set; }

        public CefFrame Frame { get; private set; }
    }
}
