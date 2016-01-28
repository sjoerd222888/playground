using System;
using CefGlueWinForms.EmbeddedBrowser.Events;
using Xilium.CefGlue;

namespace CefGlueWinForms.EmbeddedBrowser
{
    /// <summary>
    /// TODO: think about name and wheter it can be used across diferent platforms
    /// </summary>
    class MyLifeSpanHandler :CefLifeSpanHandler
    {
        private readonly EmbeddedCefBrowserCore _core;

        public MyLifeSpanHandler(EmbeddedCefBrowserCore core)
        {
            _core = core;
        }

        protected override void OnAfterCreated(CefBrowser browser)
        {
            base.OnAfterCreated(browser);

        	_core.InvokeIfRequired(() => _core.OnBrowserAfterCreated(browser));
        }

        protected override bool DoClose(CefBrowser browser)
        {
            // TODO: ... dispose core
            return false;
        }

		protected override void OnBeforeClose(CefBrowser browser)
		{
			if (_core.InvokeRequireOnUi())
                _core.MainUiDispatcher.BeginInvoke((Action)_core.OnBeforeClose);
			else
				_core.OnBeforeClose();
		}


        //TODO: there might be a change in the current version of CEF
        protected override bool OnBeforePopup(CefBrowser browser, 
            CefFrame frame, 
            string targetUrl, 
            string targetFrameName, 
            CefPopupFeatures popupFeatures, 
            CefWindowInfo windowInfo, 
            ref CefClient client, 
            CefBrowserSettings settings, 
            ref bool noJavascriptAccess)
		{
			var e = new BeforePopupEventArgs(frame, targetUrl, targetFrameName, popupFeatures, windowInfo, client, settings,
								 noJavascriptAccess);

			_core.InvokeIfRequired(() => _core.OnBeforePopup(e));

			client = e.Client;
			noJavascriptAccess = e.NoJavascriptAccess;

			return e.Handled;
		}
    }
}
