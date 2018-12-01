using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using UltraMusic.Portable.Models;
using UltraMusic.Portable.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UltraMusic.UWP.ViewModels
{
    internal class WebViewWrapper : WebViewWrapperBase
    {
        private WebView CastedWebView { get { return WebView as WebView; } }
        private DispatcherTimer timer;

        public WebViewWrapper(object webView, MusicProvider musicProvider) : base(webView, musicProvider)
        {
            InitWebView();

            if (musicProvider.Id != "AmazonPrime") return;

            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private async Task InitWebView()
        {
            CastedWebView.ScriptNotify += CastedWebView_ScriptNotify;
            CastedWebView.NavigationCompleted += CastedWebView_NavigationCompleted;
            await CastedWebView.InvokeScriptAsync("eval", new string[] { musicProvider.FunctionsJs });
        }

        private async void Timer_Tick(object sender, object e)
        {
            string js = "function tot() {if (document.isObservingPlaybackState) {return 'true';} else {return 'false';}} tot();";
            string result = await CastedWebView.InvokeScriptAsync("eval", new string[] { js });
            if (result == "true")
            {
                timer.Stop();
            }
            else
            {
                await AddObservers();
            }
        }

        private async void CastedWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            await AddObservers();
        }

        private async Task AddObservers()
        {
            await CastedWebView.InvokeScriptAsync("eval", new string[] { musicProvider.FunctionsJs });
            await CastedWebView.InvokeScriptAsync("eval", new string[] { "addObservers();" });
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
