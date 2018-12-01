using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UltraMusic.Portable.Models;

namespace UltraMusic.Portable.ViewModels
{
    public enum PlayerState
    {
        Playing,
        Paused,
        Idle
    }

    public abstract class WebViewWrapperBase
    {
        protected readonly MusicProvider musicProvider;
        public object WebView { get; }

        protected WebViewWrapperBase(object webView, MusicProvider musicProvider)
        {
            WebView = webView;
            this.musicProvider = musicProvider;
        }


        public abstract Task<object> EvaluateJavaScript(string script);

        public abstract void Navigate(string url);

        private async Task<object> SafeEvaluateJavaScript(string script)
        {
            try
            {
                return await EvaluateJavaScript(script);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        public void Search(string query)
        {
            Navigate(string.Format(musicProvider.SearchUrl, query));
        }

        #region Playback Control

        public virtual async Task<object> Play()
        {
            return await SafeEvaluateJavaScript("play();");
        }

        public virtual async Task<object> Pause()
        {
            return await SafeEvaluateJavaScript("pause();");
        }

        public virtual async Task<object> Next()
        {
            return await SafeEvaluateJavaScript("next();");
        }

        public virtual async Task<object> Previous()
        {
            return await SafeEvaluateJavaScript("previous();");
        }

        #endregion

        public virtual async Task<PlayerState> GetPlayerState()
        {
            var result = await SafeEvaluateJavaScript("getPlayerState();");
            string stateString = result.ToString();
            Enum.TryParse(stateString, true, out PlayerState state);
            return state;
        }

        public event EventHandler PlayerStateChanged;

        protected void RaisePlayerStateChanged()
        {
            PlayerStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
