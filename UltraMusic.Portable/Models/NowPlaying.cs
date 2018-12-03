using System;
using System.Collections.Generic;
using System.Text;

namespace UltraMusic.Portable.Models
{
    public class NowPlaying
    {
        public string AlbumArt { get; set; } = "";
        public string Title { get; set; } = "";
        public string Album { get; set; } = "";
        public string Artist { get; set; } = "";

        public override bool Equals(object obj)
        {
            if (!(obj is NowPlaying)) return false;
            NowPlaying o = (NowPlaying)obj;
            return
                AlbumArt == o.AlbumArt &&
                Title == o.Title &&
                Album == o.Album &&
                Artist == o.Artist;
        }

        public override int GetHashCode()
        {
            var hashCode = -978999732;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(AlbumArt);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Album);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Artist);
            return hashCode;
        }
    }
}
