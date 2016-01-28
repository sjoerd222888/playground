using System;
using System.Windows.Threading;
using CefGlueWinForms.EmbeddedBrowser.Events;
using Xilium.CefGlue;

namespace CefGlueWinForms.EmbeddedBrowser
{
    public abstract class EmbeddedCefBrowserCore : IDisposable
    {
        #region Events

        public event EventHandler BrowserCreated;
        public event EventHandler<TitleChangedEventArgs> TitleChanged;
        public event EventHandler<StatusMessageEventArgs> StatusMessage;
        public event EventHandler<ConsoleMessageEventArgs> ConsoleMessage;
        public event EventHandler<LoadingStateChangeEventArgs> LoadingStateChange;
        public event EventHandler<TooltipEventArgs> Tooltip;
        public event EventHandler BeforeClose;
        public event EventHandler<BeforePopupEventArgs> BeforePopup;
        public event EventHandler<LoadEndEventArgs> LoadEnd;
        public event EventHandler<LoadErrorEventArgs> LoadError;
        public event EventHandler<LoadStartEventArgs> LoadStarted;
        public event EventHandler<PluginCrashedEventArgs> PluginCrashed;
        public event EventHandler<RenderProcessTerminatedEventArgs> RenderProcessTerminated;
        public event EventHandler<AddressChangedEventArgs> AddressChanged;

        #endregion






        private CefBrowser browser;
        private IntPtr browserWindowHandle;
        private CefBrowserHost browserHost; //TODO: do we need  it here? better call _browser.GetHost() when needed?

        private int browserWidth;
        private int browserHeight;
        private bool browserSizeChanged;


        public IntPtr Handle { get { return browserWindowHandle; } }



        public Dispatcher MainUiDispatcher { get; private set; }


        public CefBrowser Browser { get { return browser; } } //TODO: do I need this property?

        private string browserUrl;

        public string StartUrl { get; set; }
        public string Address { get; private set; }
        public string Title { get; private set; }

        private readonly Func<bool> invokeRequiredGetter;


        protected EmbeddedCefBrowserCore(Dispatcher dispatcher, Func<bool> invokeRequiredGetter)
        {
            MainUiDispatcher = dispatcher;
            this.invokeRequiredGetter = invokeRequiredGetter;
            //StartUrl = "about:blank";
            StartUrl = "http://www.google.com";
            //StartUrl = "http://ramphastos.ramphastos.com";
        }

        public bool InvokeRequireOnUi()
        {
            return invokeRequiredGetter();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public void Dispose(bool disposing)
        {
            if (browserHost != null && disposing)
            {
                browserHost.CloseBrowser();
                browserHost.Dispose(); //TODO: correct?
                browserHost = null;
            }
            if (browser != null)
            {
                browser.Dispose();
                browser = null;
            }
            GC.SuppressFinalize(this);
        }

        public abstract void ResizeWindow(IntPtr handle);

        public abstract int Height { get; }
        public abstract int Width { get; }



        internal void InvokeIfRequired(Action a)
        {
            if (InvokeRequireOnUi())
                MainUiDispatcher.Invoke(a);
            else
                a();
        }

        public void NavigateToUrl(string url)
        {
            browserUrl = url;
            StartUrl = url;
            if (browser != null)
            {
                browser.GetMainFrame().LoadUrl(url);
            }
        }


        #region EventHandlers

        internal protected virtual void OnBrowserAfterCreated(CefBrowser browser)
        {
            this.browser = browser;
            browserWindowHandle = browser.GetHost().GetWindowHandle();
            ResizeWindow(browserWindowHandle);

            if (BrowserCreated != null)
                BrowserCreated(this, EventArgs.Empty);

            if (browserUrl != null)
            {
                NavigateToUrl(browserUrl);
            }
        }

        internal protected virtual void OnTitleChanged(TitleChangedEventArgs e)
        {
            Title = e.Title;

            var handler = TitleChanged;
            if (handler != null) handler(this, e);
        }

        internal protected virtual void OnAddressChanged(AddressChangedEventArgs e)
        {
            Address = e.Address;

            var handler = AddressChanged;
            if (handler != null) handler(this, e);
        }

        internal protected virtual void OnConsoleMessage(ConsoleMessageEventArgs e)
        {
            if (ConsoleMessage != null)
                ConsoleMessage(this, e);
            else
                e.Handled = false;
        }

        internal protected virtual void OnLoadingStateChange(LoadingStateChangeEventArgs e)
        {
            if (LoadingStateChange != null)
                LoadingStateChange(this, e);
        }


        internal protected virtual void OnTooltip(TooltipEventArgs e)
        {
            if (Tooltip != null)
                Tooltip(this, e);
            else
                e.Handled = false;
        }

        internal protected virtual void OnBeforeClose()
        {
            browserWindowHandle = IntPtr.Zero;
            if (BeforeClose != null)
                BeforeClose(this, EventArgs.Empty);
        }

        internal protected virtual void OnBeforePopup(BeforePopupEventArgs e)
        {
            if (BeforePopup != null)
                BeforePopup(this, e);
            else
                e.Handled = false;
        }

        internal protected virtual void OnLoadEnd(LoadEndEventArgs e)
        {
            if (LoadEnd != null)
                LoadEnd(this, e);
        }


        internal protected virtual void OnLoadError(LoadErrorEventArgs e)
        {
            if (LoadError != null)
                LoadError(this, e);
        }

        internal protected virtual void OnLoadStart(LoadStartEventArgs e)
        {
            if (LoadStarted != null)
                LoadStarted(this, e);
        }

        internal protected virtual void OnPluginCrashed(PluginCrashedEventArgs e)
        {
            if (PluginCrashed != null)
                PluginCrashed(this, e);
        }

        internal protected virtual void OnRenderProcessTerminated(RenderProcessTerminatedEventArgs e)
        {
            if (RenderProcessTerminated != null)
                RenderProcessTerminated(this, e);
        }

        #endregion



    }
}
