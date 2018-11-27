using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UltraMusic.UWP.Helpers;
using UltraMusic.UWP.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UltraMusic.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = ViewModelLocator.MainViewModel;
            VM.PropertyChanged += VM_PropertyChanged;
            VM.Loaded();
        }

        private void VM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(VM.MusicProviders))
                RenderMusicProviders();
        }

        private MainViewModel VM { get { return DataContext as MainViewModel; } }

        private void RenderMusicProviders()
        {
            if (VM.MusicProviders == null)
                return;

            MainNavigationView.MenuItems.Clear();
            foreach(var p in VM.MusicProviders)
            {
                MainNavigationView.MenuItems.Add(new NavigationViewItem()
                {
                    Icon = new SymbolIcon(Symbol.Play),
                    Content = p.Name,
                    Tag = p.Id
                });
            }
        }

        private void MainNavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            string providerId = ((NavigationViewItem)args.SelectedItem).Tag.ToString();
            var wrapper = VM.GetWebViewWrapper(VM.MusicProviders.Find(p => p.Id == providerId));
            sender.Content = wrapper.WebView;
        }
    }
}
