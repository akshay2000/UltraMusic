using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using UltraMusic.Portable.Models;
using Newtonsoft.Json;
using System.IO;

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
            MusicProviders = await GetProvidersAsync();
        }

        private async Task<List<MusicProvider>> GetProvidersAsync()
        {
            string specDirectory = GetProvidersSpecDirectory();
            string specJson = await GetText(Path.Combine(specDirectory, "Spec.json"));
            var providers = JsonConvert.DeserializeObject<List<MusicProvider>>(specJson);
            foreach (var provider in providers)
            {
                string id = provider.Id;
                provider.PlayJs = await GetText(specDirectory, id, "Play.js");
                provider.PauseJs = await GetText(specDirectory, id, "Pause.js");
                provider.NextJs = await GetText(specDirectory, id, "Next.js");
                provider.PreviousJs = await GetText(specDirectory, id, "Previous.js");
                provider.PlayerStateJs = await GetText(specDirectory, id, "PlayerState.js");
            }
            return providers;
        }

        public virtual string GetProvidersSpecDirectory() => throw new NotImplementedException();

        public override void Loaded()
        {
            LoadMusicProvidersAsync();
        }

        private async Task<string> GetText(params string[] fragments)
        {
            string filePath = Path.Combine(fragments);
            using (StreamReader reader = File.OpenText(filePath))
                return await reader.ReadToEndAsync();
        }
    }
}
