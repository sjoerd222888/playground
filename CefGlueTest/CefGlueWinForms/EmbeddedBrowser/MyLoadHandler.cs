﻿using CefGlueWinForms.EmbeddedBrowser.Events;
using Xilium.CefGlue;

namespace CefGlueWinForms.EmbeddedBrowser
{
    sealed class MyLoadHandler : CefLoadHandler
	{
        private readonly EmbeddedCefBrowserCore _core;

        public MyLoadHandler(EmbeddedCefBrowserCore core)
		{
			_core = core;
		}

		protected override void OnLoadEnd(CefBrowser browser, CefFrame frame, int httpStatusCode)
		{
			_core.InvokeIfRequired(() => _core.OnLoadEnd(new LoadEndEventArgs(frame, httpStatusCode)));
		}

		protected override void OnLoadError(CefBrowser browser, CefFrame frame, CefErrorCode errorCode, string errorText, string failedUrl)
		{
			_core.InvokeIfRequired(() => _core.OnLoadError(new LoadErrorEventArgs(frame, errorCode, errorText, failedUrl)));
		}

		protected override void OnLoadStart(CefBrowser browser, CefFrame frame)
		{
			_core.InvokeIfRequired(() => _core.OnLoadStart(new LoadStartEventArgs(frame)));
		}

        protected override void OnLoadingStateChange(CefBrowser browser, bool isLoading, bool canGoBack, bool canGoForward)
        {
            _core.InvokeIfRequired(() => _core.OnLoadingStateChange(new LoadingStateChangeEventArgs(isLoading, canGoBack, canGoForward)));
        }
    }
}