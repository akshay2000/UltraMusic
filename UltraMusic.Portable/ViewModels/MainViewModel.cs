using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using UltraMusic.Portable.Models;
using Newtonsoft.Json;

namespace UltraMusic.Portable.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private List<MusicProvider> musicProviders;
        public List<MusicProvider> MusicProviders
        {
            get
            {
                if (musicProviders == null)
                    LoadMusicProvidersAsync();
                return musicProviders;
            }
            set => Set(ref musicProviders, value);
        }

        public async void LoadMusicProvidersAsync()
        {
            var jsonString = await GetProvidersSpecAsync();
            MusicProviders = JsonConvert.DeserializeObject<List<MusicProvider>>(jsonString);
        }

        public virtual async Task<string> GetProvidersSpecAsync() {
            const string ret = "[{\"Id\":\"Saavn\",\"Name\":\"Saavn\",\"Url\":\"https://saavn.com\",\"PauseJs\":\"\",\"PlayJs\":\"\",\"PreviousJs\":\"\",\"NextJs\":\"\",\"SearchUrl\":\"https://saavn.com/search/{0}\"}," +
                "{\"Id\":\"AmazonPrime\",\"Name\":\"Amazon Prime\",\"Url\":\"https://music.amazon.in\",\"PauseJs\":\"\",\"PlayJs\":\"\",\"PreviousJs\":\"\",\"NextJs\":\"\",\"SearchUrl\":\"https://music.amazon.in/search/{0}\"}," +
                "{\"Id\":\"GoogleMusic\",\"Name\":\"Google Music\",\"Url\":\"https://music.google.com\",\"PauseJs\":\"\",\"PlayJs\":\"\",\"PreviousJs\":\"\",\"NextJs\":\"\",\"SearchUrl\":\"https://play.google.com/music/listen?u=0#/sr/{0}\"}]";
            await Task.CompletedTask;
            return ret;
        }

        public override void Loaded()
        {
            LoadMusicProvidersAsync();
        }
    }
}
