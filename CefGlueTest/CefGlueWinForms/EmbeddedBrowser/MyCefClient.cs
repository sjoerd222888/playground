using Xilium.CefGlue;

namespace CefGlueWinForms.EmbeddedBrowser
{
    public class MyCefClient : CefClient
    {
        private EmbeddedCefBrowserCore owner; //TODO: look if we can use an interface here
        private MyLifeSpanHandler lifeSpanHandler;
        private MyDisplayHandler displayHandler;
        private MyLoadHandler loadHandler;
        //private ? requestHandler;


        public MyCefClient(EmbeddedCefBrowserCore owner)
        {
            this.owner = owner;
            lifeSpanHandler = new MyLifeSpanHandler(owner);
            displayHandler = new MyDisplayHandler(owner);
            loadHandler = new MyLoadHandler(owner);

        }

        protected override CefLifeSpanHandler GetLifeSpanHandler()
        {
            return lifeSpanHandler;
        }

        protected override CefDisplayHandler GetDisplayHandler()
        {
            return displayHandler;
        }

        protected override CefLoadHandler GetLoadHandler()
        {
            return loadHandler;
        }

        /*protected override CefRequestHandler GetRequestHandler()
        {
            return requestHandler;
        }*/

    }
}
