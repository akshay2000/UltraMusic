using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMusic.Portable.Models;
using UltraMusic.Portable.ViewModels;
using Windows.UI.Xaml.Controls;

namespace UltraMusic.UWP.ViewModels
{
    internal class WebViewWrapper : WebViewWrapperBase
    {
        private WebView CastedWebView { get { return WebView as WebView; } }

        public WebViewWrapper(object webView, MusicProvider musicProvider) : base(webView, musicProvider)
        {
        }

        public override async Task<object> EvaluateJavaScript(string script)
        {
            return await CastedWebView.InvokeScriptAsync("eval", new string[] { script });
        }
    }
}
