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
            CastedWebView.ScriptNotify += CastedWebView_ScriptNotify;
            CastedWebView.NavigationCompleted += CastedWebView_NavigationCompleted;
            CastedWebView.DOMContentLoaded += CastedWebView_DOMContentLoaded;
            CastedWebView.ContentLoading += CastedWebView_ContentLoading;
        }

        private void CastedWebView_ContentLoading(WebView sender, WebViewContentLoadingEventArgs args)
        {
            //throw new NotImplementedException();
        }

        private void CastedWebView_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            
        }

        private async void CastedWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            var r= await sender.InvokeScriptAsync("eval", new string[] { musicProvider.EventsJs });
            var r1 = await sender.InvokeScriptAsync("eval", new string[] { "addObservers();" });
        }

        private void CastedWebView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            switch(e.Value)
            {
                case "PlaybackStateChanged":
                    RaisePlayerStateChanged();
                    break;
            }
        }

        public override async Task<object> EvaluateJavaScript(string script)
        {
            return await CastedWebView.InvokeScriptAsync("eval", new string[] { script });
        }
    }
}
