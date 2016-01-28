using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Threading;
using Xilium.CefGlue;

namespace CefGlueWinForms.EmbeddedBrowser
{
    public class EmbeddedCefBrowserControl : Control, IDisposable, IEmbeddedBrowserControl
    {
        #region Properties

        private string url;

        public string Url
        {
            get { return url; }
            set
            {
                if (value == null)
                {
                    return;
                }
                url = value;
                EmbeddedBrowser.NavigateToUrl(url);
            }
        }

        #endregion //Properties

        protected readonly EmbeddedCefBrowserCore EmbeddedBrowser;

        private bool handleCreated;



        public EmbeddedCefBrowserControl()
        {
            SetStyle(
                ControlStyles.ContainerControl
                | ControlStyles.ResizeRedraw
                | ControlStyles.FixedWidth
                | ControlStyles.FixedHeight
                | ControlStyles.StandardClick
                | ControlStyles.UserMouse
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.StandardDoubleClick
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.CacheText
                | ControlStyles.EnableNotifyMessage
                | ControlStyles.DoubleBuffer
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.UseTextForAccessibility
                | ControlStyles.Opaque,
                false);

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.Selectable,
                true);

            EmbeddedBrowser = new EmbeddedBrowserWinForms(this, Dispatcher.CurrentDispatcher, /*new Func<bool>*/(() => InvokeRequired));
        }



        [Browsable(false)]
        public CefBrowserSettings BrowserSettings { get; set; }



        public new void Dispose()
        {
            Dispose(true);
            EmbeddedBrowser.Dispose(true);
            GC.SuppressFinalize(this);
        }




        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (DesignMode)
            {
                if (!handleCreated)
                    Paint += PaintInDesignMode;
            }
            else
            {
                var windowInfo = CefWindowInfo.Create();
                windowInfo.SetAsChild(Handle, new CefRectangle { X = 0, Y = 0, Width = Width, Height = Height });

                var client = new MyCefClient(EmbeddedBrowser);

                var settings = BrowserSettings;
                if (settings == null)
                    settings = new CefBrowserSettings();

                CefBrowserHost.CreateBrowser(windowInfo, client, settings, EmbeddedBrowser.StartUrl);
            }

            handleCreated = true;
        }

        private void PaintInDesignMode(object sender, PaintEventArgs e)
        {
            var width = Width;
            var height = Height;
            if (width > 1 && height > 1)
            {
                var brush = new SolidBrush(ForeColor);
                var pen = new Pen(ForeColor);
                pen.DashStyle = DashStyle.Dash;

                e.Graphics.DrawRectangle(pen, 0, 0, width - 1, height - 1);

                var fontHeight = (int)(Font.GetHeight(e.Graphics) * 1.25);

                var x = 3;
                var y = 3;

                e.Graphics.DrawString("CefWebBrowser", Font, brush, x, y + (0 * fontHeight));
                e.Graphics.DrawString(string.Format("StartUrl: {0}", EmbeddedBrowser.StartUrl), Font, brush, x, y + (1 * fontHeight));

                brush.Dispose();
                pen.Dispose();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (EmbeddedBrowser.Handle != IntPtr.Zero)
            {
                // Ignore size changes when form are minimized.
                var form = TopLevelControl as Form;
                if (form != null && form.WindowState == FormWindowState.Minimized)
                {
                    return;
                }

                EmbeddedBrowser.ResizeWindow(EmbeddedBrowser.Handle);
            }
        }


    }
}
