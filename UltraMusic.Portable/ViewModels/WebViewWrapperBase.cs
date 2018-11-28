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

        public virtual async Task<object> Play()
        {
            return await SafeEvaluateJavaScript(musicProvider.PlayJs);
        }

        public virtual async Task<object> Pause()
        {
            return await SafeEvaluateJavaScript(musicProvider.PauseJs);
        }

        public virtual async Task<object> Next()
        {
            return await SafeEvaluateJavaScript(musicProvider.NextJs);
        }

        public virtual async Task<object> Previous()
        {
            return await SafeEvaluateJavaScript(musicProvider.PreviousJs);
        }

        public virtual async Task<PlayerState> GetPlayerState()
        {
            var result = await SafeEvaluateJavaScript(musicProvider.PlayerStateJs);
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
