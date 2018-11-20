using System;
using System.Threading.Tasks;
using Foundation;
using System.IO;
using UltraMusic.Portable.Models;
using WebKit;
using CoreGraphics;
using AppKit;
using UltraMusic.Helpers;

namespace UltraMusic.ViewModels
{
    public class MainViewModel : Portable.ViewModels.MainViewModel
    {
        public MainViewModel () :base(WrapperFactory)
        {}

        public static WebViewWrapper WrapperFactory(MusicProvider provider)
        {
            WKUserContentController controller = new WKUserContentController();
            controller.AddUserScript(
                new WKUserScript(new NSString(provider.EventsJs), WKUserScriptInjectionTime.AtDocumentEnd, false)
            );
            controller.AddScriptMessageHandler(Container.WKScriptMessageHandler, "playbackState");

            WKWebViewConfiguration config = new WKWebViewConfiguration
            {
                UserContentController = controller
            };
            config.Preferences.SetValueForKey(NSObject.FromObject(true), new NSString("developerExtrasEnabled"));

            var webView = new WKWebView(new CGRect(0, 0, 100, 100), config)
            {
                CustomUserAgent = "Mozilla/5.0 " +
                    "(Macintosh; Intel Mac OS X 10_13_6) AppleWebKit/605.1.15 " +
                    "(KHTML, like Gecko) Version/12.0 Safari/605.1.15",
                AutoresizingMask = NSViewResizingMask.HeightSizable | NSViewResizingMask.WidthSizable,
                NavigationDelegate = new NavigationDelegate()
            };

            webView.TranslatesAutoresizingMaskIntoConstraints = false;
            var req = new NSUrlRequest(new NSUrl(provider.Url));
            webView.LoadRequest(req);

            return new WebViewWrapper(webView, provider);
        }



        public override string GetProvidersSpecDirectory()
        {
            return Path.Combine(NSBundle.MainBundle.ResourcePath, "ProvidersSpec");
        }
    }

    internal class NavigationDelegate : WKNavigationDelegate
    {
        public async override void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            await webView.EvaluateJavaScriptAsync("addObservers();");
        }
    }
}
