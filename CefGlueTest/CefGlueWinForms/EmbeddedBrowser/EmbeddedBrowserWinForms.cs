using System;
using System.Windows.Forms;
using System.Windows.Threading;
using CefGlueWinForms.EmbeddedBrowser.Native;

namespace CefGlueWinForms.EmbeddedBrowser
{
    public class EmbeddedBrowserWinForms : EmbeddedCefBrowserCore
    {
        private readonly Control parent;
        public EmbeddedBrowserWinForms(Control parent, Dispatcher dispatcher, Func<bool> invokeRequiredGetter)
            : base(dispatcher, invokeRequiredGetter)
        {
            this.parent = parent;
        }

        public override void ResizeWindow(IntPtr handle)
        {
            if (handle != IntPtr.Zero)
            {
                NativeMethods.SetWindowPos(handle, IntPtr.Zero,
                    0, 0, Width, Height,
                    SetWindowPosFlags.NoMove | SetWindowPosFlags.NoZOrder
                    );
            }
        }

        public override int Height
        {
            get { return parent.Height; }
        }

        public override int Width
        {
            get { return parent.Width; }
        }
    }
}
