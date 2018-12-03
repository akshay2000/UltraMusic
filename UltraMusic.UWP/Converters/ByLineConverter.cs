using System;
using UltraMusic.Portable.Models;
using Windows.UI.Xaml.Data;

namespace UltraMusic.UWP.Converters
{
    public class ByLineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            NowPlaying nowPlaying = (NowPlaying)value;
            const int maxArtists = 2;
            string[] artists = nowPlaying.Artist.Split(',');
            string artist;
            if (artists.Length > maxArtists)
            {
                int remainingArtists = artists.Length - maxArtists;
                string otherString = remainingArtists == 1 ? "other" : "others";
                artist = $"{artists[0]}, {artists[1]} and {remainingArtists} {otherString}";
            }
            else
            {
                artist = nowPlaying.Artist;
            }
            string album = System.Net.WebUtility.HtmlDecode(nowPlaying.Album);
            string fromLine = $"from {album}";
            if (!string.IsNullOrEmpty(artist))
                return $"by {artist} {fromLine}";
            else
                return fromLine;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
