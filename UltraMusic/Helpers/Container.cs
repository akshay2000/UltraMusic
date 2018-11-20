using System;
using WebKit;

namespace UltraMusic.Helpers
{
    public class Container
    {
        public Container()
        {
        }

        public static IWKScriptMessageHandler WKScriptMessageHandler { get; set; }
    }
}
