using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMusic.Portable.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace UltraMusic.UWP.Converters
{
    public class PlaybackStateToSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            PlayerState state = (PlayerState)value;
            switch (state)
            {
                default:
                case PlayerState.Idle:
                case PlayerState.Paused:
                    return new SymbolIcon(Symbol.Play);
                case PlayerState.Playing:
                    return new SymbolIcon(Symbol.Pause);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
