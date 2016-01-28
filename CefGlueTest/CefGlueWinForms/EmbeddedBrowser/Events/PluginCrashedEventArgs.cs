using System;

namespace CefGlueWinForms.EmbeddedBrowser.Events
{
    public class PluginCrashedEventArgs : EventArgs
    {
        public PluginCrashedEventArgs(string pluginPath)
        {
            PluginPath = pluginPath;
        }

        public string PluginPath { get; private set; }
    }
}
