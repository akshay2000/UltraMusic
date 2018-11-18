using System;
using System.Threading.Tasks;
using UltraMusic.Portable.Models;
using WebKit;

namespace UltraMusic.ViewModels
{
    public class WebViewWrapper : Portable.ViewModels.WebViewWrapperBase
    {
        public WKWebView WebView { get; }
        public WebViewWrapper(WKWebView webView, MusicProvider provider) : base(provider)
        {
            this.WebView = webView;
        }

        public override async Task<object> EvaluateJavaScript(string script)
        {
            var result = await WebView.EvaluateJavaScriptAsync(script);
            return result;
        }
    }
}
