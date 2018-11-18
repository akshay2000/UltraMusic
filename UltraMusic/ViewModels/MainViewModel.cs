using System;
using System.Threading.Tasks;
using Foundation;
using System.IO;

namespace UltraMusic.ViewModels
{
    public class MainViewModel : Portable.ViewModels.MainViewModel
    {
        public override string GetProvidersSpecDirectory()
        {
            return Path.Combine(NSBundle.MainBundle.ResourcePath, "ProvidersSpec");
        }
    }
}
