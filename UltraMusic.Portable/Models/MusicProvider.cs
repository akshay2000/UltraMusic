namespace UltraMusic.Portable.Models
{
    public class MusicProvider
    {
        public string Id { get; private set; }
        public string Url { get; private set; }
        public string Name { get; set; }
        public string PauseJs { get; private set; }
        public string PlayJs { get; private set; }
        public string PreviousJs { get; private set; }
        public string NextJs { get; private set; }
        public string SearchUrl { get; private set; }
    }
}