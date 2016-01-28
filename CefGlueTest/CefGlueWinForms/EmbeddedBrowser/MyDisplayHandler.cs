using System;
using Xilium.CefGlue;

namespace CefGlueWinForms.EmbeddedBrowser
{
    //TODO: Check if this can be technology independent
    class MyDisplayHandler : CefDisplayHandler
    {
        private EmbeddedCefBrowserCore owner;

        public MyDisplayHandler(EmbeddedCefBrowserCore owner)
        {
            if (owner == null) throw new ArgumentNullException("owner");
            this.owner = owner;
        }

        //TODO: here I could overwrite some methods... see WpfCefDisplayHandler in Xilium.CefGlue.WPF

    }
}
