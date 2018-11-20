using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Foundation;
using UltraMusic.Helpers;
using UltraMusic.Portable.Models;
using WebKit;

namespace UltraMusic.ViewModels
{
    public class WebViewWrapper : Portable.ViewModels.WebViewWrapperBase
    {
        public IntPtr Handle { get; set; }

        public WebViewWrapper(WKWebView webView, MusicProvider provider) : base(webView, provider)
        { }

        public override async Task<object> EvaluateJavaScript(string script)
        {
            var result = await ((WKWebView)WebView).EvaluateJavaScriptAsync(script);
            return result;
        }
    }
}
