namespace UltraMusic.Portable.Models
{
    public class MusicProvider
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string PauseJs { get; set; }
        public string PlayJs { get; set; }
        public string PreviousJs { get; set; }
        public string NextJs { get; set; }
        public string SearchUrl { get; set; }
        public string PlayerStateJs { get; set; }
    }
}